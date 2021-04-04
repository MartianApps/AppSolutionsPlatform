using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public class PropertyChangeCommand : AbstractBaseCommand
    {
        private object _newValue;
        private object _oldValue;
        private object _model;
        private PropertyInfo _propertyInfo;

        public static PropertyChangeCommand Create(string propertyName, object newValue, object model)
        {            
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }            

            var cmd = new PropertyChangeCommand();
            cmd.PropertyName = propertyName;
            cmd._newValue = newValue;
            cmd._model = model;

            var properties = model.GetType().GetProperties();
            cmd._propertyInfo = properties.FirstOrDefault(o => o.Name == propertyName);
            if (cmd._propertyInfo == null)
            {
                throw new ArgumentException($"type {model.GetType().Name} does have no property with name '{propertyName}'");
            }
            if (!cmd._propertyInfo.CanWrite)
            {
                throw new ArgumentException($"property with name '{propertyName}' of type {model.GetType().Name} cannot be written");
            }
            if (!cmd._propertyInfo.CanRead)
            {
                throw new ArgumentException($"property with name '{propertyName}' of type {model.GetType().Name} cannot be read");
            }

            return cmd;
        }

        public string PropertyName { get; private set; }

        public override void Execute()
        {
            _oldValue = _propertyInfo.GetValue(_model);

            _propertyInfo.SetValue(_model, _newValue);
        }

        public override void Undo()
        {
            _propertyInfo.SetValue(_model, _oldValue);
        }
    }
}
