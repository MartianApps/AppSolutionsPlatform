using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.Commands
{
    public class CommandProcessor
    {
        self.undoStack = [];
                self.redoStack = [];
                self.breakNextCompoundFlag = false;
                self.breakNextMergeFlag = false;

        // use carefully!!
            eraseLastUndoCmd: function()
        {
            this.undoStack.pop();
        },

            // use carefully!!
            emptyStacks: function()
        {
            this.undoStack = [];
            this.redoStack = [];
            this.breakNextCompoundFlag = false;
            this.breakNextMergeFlag = false;
        },

            breakNextCompound: function()
        {
            this.breakNextCompoundFlag = true;
        },

            breakNextMerge: function()
        {
            this.breakNextMergeFlag = true;
        },

            execute: function(command)
        {
            this.redoStack = [];

            // execute command
            command.execute();

            // raise event for this
            var eventData = command.getEventData();
            eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.EXECUTE;
            PubSub.publish(eventData.topic, eventData);

            // check if it is mergable with previous command
            // create compound if YES and in case it is not already a compount command
            if (this.undoStack.length > 0)
            {
                var lastCommand = this.undoStack.pop();
                // check das letzte Command ob es mit dem neuen Command gruppiert werden will
                if (lastCommand.canBeCompoundedWith(command) && !this.breakNextCompoundFlag)
                {
                    // Gruppierung !!!!
                    // Ist das letzte bereits ein Compound?
                    if (!lastCommand.isCompound())
                    {
                        // Noch nicht ... erstmal in eines umwandeln
                        lastCommand = CompoundCommand.create(lastCommand);
                    }
                    // Jetzt haben wir im letzten Command definitiv ein Compound ... das aktuelle Command nur noch adden
                    lastCommand.append(command);
                    this.undoStack.push(lastCommand);

                }
                else if (lastCommand.isMergableWith(command) && !this.breakNextMergeFlag)
                {
                    lastCommand.mergeWith(command);
                    this.undoStack.push(lastCommand);
                }
                else
                {
                    // Nein, keine Gruppierung und kein Merge ...
                    // letztes Command wieder in Liste und das aktuelle oben drauf
                    this.undoStack.push(lastCommand);
                    this.undoStack.push(command);
                }
            }
            else
            {
                this.undoStack.push(command);
            }
            this.breakNextCompoundFlag = false;
            this.breakNextMergeFlag = false;
        },

            undo: function()
        {
            if (this.undoStack.length <= 0)
            {
                return;
            }

            var cmd = this.undoStack.pop();
            cmd.undo();
            this.redoStack.push(cmd);

            // raise event for this
            var eventData = command.getEventData();
            eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.UNDO;
            PubSub.publish(eventData.topic, eventData);
        },

            redo: function()
        {
            if (this.redoStack.length <= 0)
            {
                return;
            }
            var cmd = this.redoStack.pop();
            cmd.redo();
            this.undoStack.push(cmd);

            // raise event for this
            var eventData = command.getEventData();
            eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.REDO;
            PubSub.publish(eventData.topic, eventData);
        }
    }
}
