using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public class CompoundCommand : AbstractBaseCommand
    {
        private List<ICommand> _commandList = new List<ICommand>();
        private Dictionary<Guid, ICommand> _linkDict = new Dictionary<Guid, ICommand>();
        private bool _mergableByDefault = false;

        public CompoundCommand Append(ICommand command)
        {
            _commandList.Add(command);
            return this; // chainable!
        }

        public override bool IsCompound => true;

        public override void Execute()
        {
            for (var i = 0; i < _commandList.Count; i++)
            {
                // do we need to inject model guid before execution?
                if (_linkDict.ContainsKey(_commandList[i].Id))
                {
                    // YES ... inject model from previous command
                    _commandList[i].InjectModelGuid(_linkDict[_commandList[i].Id].ModelGuid);
                }
                // NOW it is safe to execute
                _commandList[i].Execute();

                // raise event for this
                //var eventData = this.commandList[i].getEventData();
                //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.EXECUTE;
                //PubSub.publish(eventData.topic, eventData);
            }
        }

        public static CompoundCommand Create(ICommand command = null)
        {
            var self = new CompoundCommand();
            // Allow empty initialization
            if (command != null)
            {
                self._commandList.Add(command);
            }
            return self;
        }

        public static CompoundCommand CreateMergable(ICommand command=null)
        {
            var self = new CompoundCommand();
            self._mergableByDefault = true;
            // Allow empty initialization
            if (command != null)
            {
                self._commandList.Add(command);
            }
            return self;
        }

        public override void Undo()
        {
            for (var i = _commandList.Count - 1; i >= 0; i--)
            {
                _commandList[i].Undo();

                // raise event for this
                //var eventData = this.commandList[i].getEventData();
                //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.UNDO;
                //PubSub.publish(eventData.topic, eventData);
            }
        }

        public override void Redo()
        {
            // no model guid injection needed here, because everyone has already the proper
            // model guid and they stay the same during undo/redo processing
            for (var i = 0; i < _commandList.Count; i++)
            {
                // NOW it is safe to execute
                _commandList[i].Redo();

                // raise event for this
                //var eventData = this.commandList[i].getEventData();
                //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.REDO;
                //PubSub.publish(eventData.topic, eventData);
            }
        }

        public CompoundCommand InjectModelToWorkOnFrom(ICommand command)
        {
            var lastCmd = _commandList[_commandList.Count - 1];
            // Add link to command where the model guid has to be loaded from
            // command hast to be of type BaseObjectInstantiationCommand to make this work
            // lastCmd has to be of type BaseModelChangeCommand to make this work (hast to work on a model not yet created!)
            // IMPORTANT:
            // command has to be a previous command in commandList (so model gets created before the change command works on it)
            _linkDict[lastCmd.Id] = command;

            return this; // chainable!
        }

        public override bool IsMergableWith(ICommand command)
        {
            if (_mergableByDefault)
            {
                // But: both need to be a compound!
                if (command is CompoundCommand)
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
                return _commandList[0].IsMergableWith(command);
            }
        }

        public override void MergeWith(ICommand command)
        {
            if (!IsMergableWith(command) || !(command is CompoundCommand))
            {
                throw new ArgumentException("commands not mergable");
            }

            var cpdCommand = command as CompoundCommand;

            if (_commandList.Count == 0 || cpdCommand._commandList.Count == 0 || _commandList.Count != cpdCommand._commandList.Count)
            {
                throw new ArgumentException("compound commands not mergable because of different command counts");
            }

            do
            {
                var index = cpdCommand._commandList.Count - 1;
                var cmdToMerge = cpdCommand._commandList[index];
                cpdCommand._commandList.RemoveAt(index);
                // search for the cmd with which it can be merged
                var merged = false;
                for (var i = 0; i < _commandList.Count; i++)
                {
                    if (_commandList[i].IsMergableWith(cmdToMerge))
                    {
                        _commandList[i].MergeWith(cmdToMerge);
                        merged = true;
                        break;
                    }
                }
                if (!merged)
                {
                    throw new ArgumentException("compound commands about to merge contain non-mergable objects!!!");
                }
            } while (cpdCommand._commandList.Count > 0);
        }

        //getStartCmdType: function()
        //{
        //    if (this.commandList.length == 0)
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return this.commandList[0].type;
        //    }
        //}
    }
}
