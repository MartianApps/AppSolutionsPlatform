using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public class CompoundCommand : AbstractBaseCommand
    {
        private Stack<ICommand> _commandList = new Stack<ICommand>();
        public CompoundCommand Append(ICommand command)
        {
            _commandList.Push(command);
            return this; // chainable!
        }

        public override bool IsCompound => true;

        public override void Execute()
        {
            for (var i = 0; i < _commandList.Count; i++)
            {
                // do we need to inject model guid before execution?
                if (this.linkDict.hasOwnProperty(this.commandList[i].guid))
                {
                    // YES ... inject model from previous command
                    this.commandList[i].injectModelGuid(this.linkDict[this.commandList[i].guid].getModelGuid());
                }
                // NOW it is safe to execute
                var cmd = _commandList.Pop();
                execute();

                // raise event for this
                var eventData = this.commandList[i].getEventData();
                eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.EXECUTE;
                PubSub.publish(eventData.topic, eventData);
            }
        }

        createMergable: function(command)
        {
            var self = BaseCommand.create.call(this, CommandTypes.Compound);
            self.commandList = [];
            self.linkDict = { };
            self.mergableByDefault = true;
            // Allow empty initialization
            if (command != null)
            {
                self.commandList.push(command);
            }
            return self;
        }

        undo: function()
        {
            for (var i = this.commandList.length - 1; i >= 0; i--)
            {
                this.commandList[i].undo();

                // raise event for this
                var eventData = this.commandList[i].getEventData();
                eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.UNDO;
                PubSub.publish(eventData.topic, eventData);
            }
        }

        redo: function()
        {
            // no model guid injection needed here, because everyone has already the proper
            // model guid and they stay the same during undo/redo processing
            for (var i = 0; i < this.commandList.length; i++)
            {
                // NOW it is safe to execute
                this.commandList[i].redo();

                // raise event for this
                var eventData = this.commandList[i].getEventData();
                eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.REDO;
                PubSub.publish(eventData.topic, eventData);
            }
        }

        injectModelToWorkOnFrom: function(command)
        {
            var lastCmd = this.commandList.pop();
            // Add link to command where the model guid has to be loaded from
            // command hast to be of type BaseObjectInstantiationCommand to make this work
            // lastCmd has to be of type BaseModelChangeCommand to make this work (hast to work on a model not yet created!)
            // IMPORTANT:
            // command has to be a previous command in commandList (so model gets created before the change command works on it)
            this.linkDict[lastCmd.guid] = command;

            this.commandList.push(lastCmd);
            return this; // chainable!
        },

            isMergableWith: function(withCommand)
        {
            if (this.mergableByDefault)
            {
                // But: both need to be a compound!
                if (withCommand.type === CommandTypes.Compound)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return this.commandList[0].isMergableWith(withCommand);
            }
        },

            mergeWith: function(withCommand)
        {
            if (!this.isMergableWith(withCommand))
            {
                console.log("not mergable ... WTF?");
                return;
            }

            if (this.commandList.length == 0 || withCommand.commandList.length == 0 || this.commandList.length != withCommand.commandList.length)
            {
                console.log("compound commands not mergable because of different command counts");
                return;
            }

            do
            {
                cmdToMerge = withCommand.commandList.pop();
                // search for the cmd with which it can be merged
                var merged = false;
                for (var i = 0; i < this.commandList.length; i++)
                {
                    if (this.commandList[i].isMergableWith(cmdToMerge))
                    {
                        this.commandList[i].mergeWith(cmdToMerge);
                        merged = true;
                        break;
                    }
                }
                if (!merged)
                {
                    console.log("compound commands about to merge contain non-mergable objects!!!");
                }
            } while (withCommand.commandList.length > 0);
        }

        getStartCmdType: function()
        {
            if (this.commandList.length == 0)
            {
                return "";
            }
            else
            {
                return this.commandList[0].type;
            }
        }
    }
}
