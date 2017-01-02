using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvvmUtil.PropertyChanged
{
    public class PropertyChangedAction
    {
        public PropertyChangedAction(object instance, string propertyName, Action action) 
            : this((INotifyPropertyChanged)instance, propertyName, action) { }

        public PropertyChangedAction(INotifyPropertyChanged notifyInstance, string propertyName, Action action)
        {
            // Register an event listener
            notifyInstance.PropertyChanged += (sender, args) =>
            {
                // Check the property name
                if (args.PropertyName.Equals(propertyName))
                {
                    action.Invoke();
                }
            };
        }

    }
}
