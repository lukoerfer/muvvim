using System;
using System.ComponentModel;

namespace MvvmUtil.PropertyChanged
{
    /// <summary>
    /// Provides the possibility to invoke actions on property changes
    /// </summary>
    public static class PropertyChangedAction
    {
        /// <summary>
        /// Registers an action for changes of a property
        /// </summary>
        /// <param name="instance">The instance, which notifies about property changes, must be of type INotifyPropertyChanged</param>
        /// <param name="propertyName">The name of the property whose changes to handle</param>
        /// <param name="action">The action to invoke on property changes</param>
        public static void RegisterAction(object instance, string propertyName, Action action)
        {
            RegisterAction((INotifyPropertyChanged)instance, propertyName, action);
        }

        /// <summary>
        /// Registers an action for changes of a property
        /// </summary>
        /// <param name="notifyInstance">The instance, which notifies about property changes</param>
        /// <param name="propertyName">The name of the property whose changes to handle</param>
        /// <param name="action">The action to invoke on property changes</param>
        public static void RegisterAction(INotifyPropertyChanged notifyInstance, string propertyName, Action action)
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
