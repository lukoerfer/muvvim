using MvvmUtil.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvvmUtil.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged, IRaisePropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged.Invoke(this, args);
        }
    }
}
