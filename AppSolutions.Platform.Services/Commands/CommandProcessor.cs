using AppSolutions.Platform.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.Commands
{
    public class CommandProcessor
    {
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();
        private bool _breakNextCompoundFlag = false;
        private bool _breakNextMergeFlag = false;

        // use carefully!!
        public void EraseLastUndoCmd()
        {
            _undoStack.Pop();
        }

        // use carefully!!
        public void EmptyStacks()
        {
            _undoStack = new Stack<ICommand>();
            _redoStack = new Stack<ICommand>();
            _breakNextCompoundFlag = false;
            _breakNextMergeFlag = false;
        }

        public void BreakNextCompound()
        {
            _breakNextCompoundFlag = true;
        }

        public void BreakNextMerge()
        {
            _breakNextMergeFlag = true;
        }

        public void Execute(ICommand command)
        {
            _redoStack = new Stack<ICommand>();

            // execute command
            command.Execute();

            // raise event for this
            //var eventData = command.getEventData();
            //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.EXECUTE;
            //PubSub.publish(eventData.topic, eventData);

            // check if it is mergable with previous command
            // create compound if YES and in case it is not already a compount command
            if (_undoStack.Count > 0)
            {
                var lastCommand = _undoStack.Pop();
                // check das letzte Command ob es mit dem neuen Command gruppiert werden will
                if (lastCommand.CanBeCompoundedWith(command) && !_breakNextCompoundFlag)
                {
                    // Gruppierung !!!!
                    // Ist das letzte bereits ein Compound?
                    if (!lastCommand.IsCompound)
                    {
                        // Noch nicht ... erstmal in eines umwandeln
                        lastCommand = CompoundCommand.Create(lastCommand);
                    }
                    // Jetzt haben wir im letzten Command definitiv ein Compound ... das aktuelle Command nur noch adden
                    (lastCommand as CompoundCommand).Append(command);
                    _undoStack.Push(lastCommand);

                }
                else if (lastCommand.IsMergableWith(command) && !_breakNextMergeFlag)
                {
                    lastCommand.MergeWith(command);
                    _undoStack.Push(lastCommand);
                }
                else
                {
                    // Nein, keine Gruppierung und kein Merge ...
                    // letztes Command wieder in Liste und das aktuelle oben drauf
                    _undoStack.Push(lastCommand);
                    _undoStack.Push(command);
                }
            }
            else
            {
                _undoStack.Push(command);
            }
            _breakNextCompoundFlag = false;
            _breakNextMergeFlag = false;
        }

        public void Undo()
        {
            if (_undoStack.Count <= 0)
            {
                return;
            }

            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);

            // raise event for this
            //var eventData = command.getEventData();
            //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.UNDO;
            //PubSub.publish(eventData.topic, eventData);
        }

        public void Redo()
        {
            if (_redoStack.Count <= 0)
            {
                return;
            }
            var cmd = _redoStack.Pop();
            cmd.Redo();
            _undoStack.Push(cmd);

            // raise event for this
            //var eventData = command.getEventData();
            //eventData.operation = DataModel.CORE.ENUM.COMMAND_OPERATION_TYPE.REDO;
            //PubSub.publish(eventData.topic, eventData);
        }
    }
}
