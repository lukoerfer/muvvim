using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> Execution;

        public RelayCommand(Action<T> execution)
        {
            this.Execution = execution;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(T parameter)
        {
            this.Execution.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            this.Execution.Invoke((T)parameter);
        }
    }
}
