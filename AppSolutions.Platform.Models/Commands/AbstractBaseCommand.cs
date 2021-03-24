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
            ModelGuid = Guid.Empty;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ModelGuid { get; set; }

        public void InjectModelGuid(Guid modelGuid)
        {
            ModelGuid = modelGuid;
        }

        public virtual bool IsCompound => false;

        public virtual bool CanBeCompoundedWith(ICommand command)
        {
            return false;
        }

        public virtual bool IsMergableWith(ICommand command)
        {
            return false;
        }

        /// <summary>
        /// not implemented is ok for most cases.
        /// </summary>
        /// <param name="command"></param>
        public virtual void MergeWith(ICommand command)
        {
        }

        public abstract void Execute();

        public abstract void Undo();

        /// <summary>
        /// default implementation. Might be all you need in some cases
        /// </summary>
        public virtual void Redo()
        {
            Execute();
        }
    }
}
