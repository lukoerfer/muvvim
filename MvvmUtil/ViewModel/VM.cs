using MvvmUtil.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvvmUtil.ViewModel
{
    public abstract class VM<Model> : INotifyPropertyChanged, IRaisePropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Model Instance;

        protected VM()
        {
            this.Instance = Activator.CreateInstance<Model>();
        }

        protected VM(Model instance)
        {
            this.Instance = instance;
        }

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged.Invoke(this, args);
        }
    }
}
