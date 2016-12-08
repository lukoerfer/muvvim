using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtil.PropertyChanged
{
    public class PropertyChangedChildRegister
    {
        private const string Separator = ".";

        private IRaisePropertyChanged RaiseInstance;

        public PropertyChangedChildRegister(IRaisePropertyChanged raiseInstance)
        {
            this.RaiseInstance = raiseInstance;
        }

        public void RegisterProperty(string propertyName, INotifyPropertyChanged property)
        {
            property.PropertyChanged += (sender, args) => this.HandlePropertyChanged(propertyName, args);
        }

        private void HandlePropertyChanged(string parentPropertyName, PropertyChangedEventArgs args)
        {
            string childPropertyName = string.Join(Separator, parentPropertyName, args.PropertyName);
            this.RaiseInstance.RaisePropertyChanged(new PropertyChangedEventArgs(childPropertyName));
        }
    }
}
