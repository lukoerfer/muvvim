using MvvmUtil.PropertyChanged;
using System.ComponentModel;

namespace MvvmUtil.ViewModel
{
    public abstract class ViewModel : INotifyPropertyChanged, IRaisePropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged.Invoke(this, args);
        }
    }
}
