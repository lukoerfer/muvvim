using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

using MvvmUtil.Util;

namespace MvvmUtil.Windows
{
    public class Windowed<VM> : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VM Context { get; private set; }

        private Window Frame;

        public Windowed()
        {
            this.Context = Activator.CreateInstance<VM>();
            // Register context PropertyChanged if supported
            INotifyPropertyChanged contextHandler = this.Context as INotifyPropertyChanged;
            if (contextHandler != null)
            {
                contextHandler.PropertyChanged += this.OnContextPropertyChanged;
            }
            // Create the window
            this.Frame = new Window();
            this.Frame.DataContext = this.Context;
            this.Frame.Show();
        }

        private void OnContextPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this,
                    new PropertyChangedEventArgs(string.Join(Separators.Point, nameof(this.Context), args.PropertyName)));
            }
        }

        public void Dispose()
        {
            // Unregister context PropertyChanged
            INotifyPropertyChanged contextHandler = this.Context as INotifyPropertyChanged;
            if (contextHandler != null)
            {
                contextHandler.PropertyChanged -= this.OnContextPropertyChanged;
            }
            // Close the window
            this.Frame.Close();
            this.Frame = null;
        }
    }
}
