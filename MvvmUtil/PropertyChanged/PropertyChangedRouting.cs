using MvvmUtil.Util;
using System.ComponentModel;

namespace MvvmUtil.PropertyChanged
{
    /// <summary>
    /// Provides the possibility to route PropertyChanged events of child properties
    /// </summary>
    public class PropertyChangedRouting
    {
        private readonly IRaisePropertyChanged RaiseInstance;

        /// <summary>
        /// Creates a new PropertyChanged child register
        /// </summary>
        /// <param name="raiseInstance">The instance which raises the PropertyChanged events</param>
        public PropertyChangedRouting(IRaisePropertyChanged raiseInstance)
        {
            RaiseInstance = raiseInstance;
        }

        /// <summary>
        /// Registers the child properties of a property
        /// </summary>
        /// <param name="property">The parent property</param>
        /// <param name="propertyName">The name of the parent property</param>
        public void Register(INotifyPropertyChanged property, string propertyName)
        {
            property.PropertyChanged += (sender, args) => HandlePropertyChanged(propertyName, args);
        }

        private void HandlePropertyChanged(string parentPropertyName, PropertyChangedEventArgs args)
        {
            string childPropertyName = string.Join(Separators.Point, parentPropertyName, args.PropertyName);
            RaiseInstance.RaisePropertyChanged(new PropertyChangedEventArgs(childPropertyName));
        }
    }
}
