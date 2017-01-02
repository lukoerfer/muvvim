using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmUtil.Windows
{
    public abstract class Windowed : IDisposable
    {
        private Window Frame;

        protected Windowed()
        {
            this.Frame = new Window();
            this.Frame.DataContext = this;
            this.Frame.Show();
        }

        public void Dispose()
        {
            this.Frame.Close();
            this.Frame = null;
        }
    }
}
