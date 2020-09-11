using System.ComponentModel;

namespace Muvvim.PropertyChanged
{
    /// <summary>
    /// Defines an interface to raise PropertyChanged events
    /// </summary>
    public interface IRaisePropertyChanged
    {
        /// <summary>
        /// Should be implemented to raise a PropertyChanged event for a property
        /// </summary>
        /// <param name="args">Event arguments with the property name</param>
        void RaisePropertyChanged(PropertyChangedEventArgs args);
    }
}
