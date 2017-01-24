using MvvmUtil.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtil.PropertyChanged
{
    /// <summary>
    /// Provides the possibility to route PropertyChanged events of child properties
    /// </summary>
    public class PropertyChangedChildRegister
    {
        private IRaisePropertyChanged RaiseInstance;

        /// <summary>
        /// Creates a new PropertyChanged child router
        /// </summary>
        /// <param name="instance">The instance which raises the PropertyChanged events, must be IRaisePropertyChanged</param>
        public PropertyChangedChildRegister(object instance)
            : this((IRaisePropertyChanged)instance) { }

        /// <summary>
        /// Creates a new PropertyChanged child register
        /// </summary>
        /// <param name="raiseInstance">The instance which raises the PropertyChanged events</param>
        public PropertyChangedChildRegister(IRaisePropertyChanged raiseInstance)
        {
            this.RaiseInstance = raiseInstance;
        }

        /// <summary>
        /// Registers the child properties of a property
        /// </summary>
        /// <param name="property">The parent property, must be INotifyPropertyChanged</param>
        /// <param name="propertyName">The name of the parent property</param>
        public void RegisterChildsOf(object property, string propertyName)
        {
            this.RegisterChildsOf((INotifyPropertyChanged)property, propertyName);
        }

        /// <summary>
        /// Registers the child properties of a property
        /// </summary>
        /// <param name="property">The parent property</param>
        /// <param name="propertyName">The name of the parent property</param>
        public void RegisterChildsOf(INotifyPropertyChanged property, string propertyName)
        {
            property.PropertyChanged += (sender, args) => this.HandlePropertyChanged(propertyName, args);
        }

        private void HandlePropertyChanged(string parentPropertyName, PropertyChangedEventArgs args)
        {
            string childPropertyName = string.Join(Separators.Point, parentPropertyName, args.PropertyName);
            this.RaiseInstance.RaisePropertyChanged(new PropertyChangedEventArgs(childPropertyName));
        }
    }
}
