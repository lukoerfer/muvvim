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

        public void Execute(object parameter)
        {
            if (parameter is T)
            {
                this.Execution.Invoke((T)parameter);
            }
            else
            {
                throw new ArgumentException("Wrong parameter type", "parameter");
            }
        }

        public void Execute(T parameter)
        {
            this.Execution.Invoke(parameter);
        }
    }
}
