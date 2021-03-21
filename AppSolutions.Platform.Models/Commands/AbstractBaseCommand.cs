using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    [DataContract]
    public abstract class AbstractBaseCommand : ICommand
    {
        public AbstractBaseCommand()
        {
            Id = Guid.NewGuid();
        }

        [DataMember]
        public Guid Id { get; set; }

        public virtual bool IsCompound => false;

        public virtual bool CanBeCompoundedWith(ICommand command)
        {
            return false;
        }

        public bool IsMergableWith(ICommand command)
        {
            return false;
        }

        /// <summary>
        /// not implemented is ok for most cases.
        /// </summary>
        /// <param name="command"></param>
        public void MergeWith(ICommand command)
        {
        }

        public abstract void Execute();

        public abstract void Undo();

        /// <summary>
        /// default implementation. Might be all you need in some cases
        /// </summary>
        public void Redo()
        {
            Execute();
        }
    }
}
