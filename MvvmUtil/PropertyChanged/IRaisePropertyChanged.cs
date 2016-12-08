using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmUtil.PropertyChanged
{
    public interface IRaisePropertyChanged
    {
        void RaisePropertyChanged(PropertyChangedEventArgs args);
    }
}
