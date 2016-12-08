using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action Execution;

        public RelayCommand(Action execution)
        {
            this.Execution = execution;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.Execution.Invoke();
        }

        public void Execute()
        {
            this.Execution.Invoke();
        }
    }
}
