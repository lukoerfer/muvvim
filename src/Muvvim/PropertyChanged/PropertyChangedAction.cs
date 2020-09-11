using System;
using System.ComponentModel;

namespace Muvvim.PropertyChanged
{
    /// <summary>
    /// Provides the possibility to invoke actions on property changes
    /// </summary>
    public class PropertyChangedHandler
    {
        private readonly INotifyPropertyChanged NotifyInstance;

        public PropertyChangedHandler(INotifyPropertyChanged notifyInstance)
        {
            NotifyInstance = notifyInstance;
        }

        /// <summary>
        /// Registers an action for changes of a property
        /// </summary>
        /// <param name="propertyName">The name of the property whose changes to handle</param>
        /// <param name="action">The action to invoke on property changes</param>
        public void OnPropertyChange(string propertyName, Action action)
        {
            // Register an event listener
            NotifyInstance.PropertyChanged += (sender, args) =>
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
