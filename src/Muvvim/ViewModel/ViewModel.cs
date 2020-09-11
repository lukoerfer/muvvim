using Muvvim.PropertyChanged;
using System.ComponentModel;

namespace Muvvim.ViewModel
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
