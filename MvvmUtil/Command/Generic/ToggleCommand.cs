using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    public class ToggleCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool IsActive;

        private Action<T> Execution;

        public ToggleCommand(Action<T> execution)
        {
            this.Execution = execution;
        }

        public void SetActive(bool active)
        {
            if (this.IsActive != active)
            {
                this.IsActive = active;
                this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void ToggleActive()
        {
            this.SetActive(!this.IsActive);
        }

        public bool CanExecute()
        {
            return this.IsActive;
        }

        public bool CanExecute(T parameter)
        {
            return this.IsActive;
        }

        public bool CanExecute(object parameter)
        {
            return this.IsActive;
        }

        public void Execute(T parameter)
        {
            this.Execution.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            if (!(parameter is T))
            {
                throw new ArgumentException("Wrong parameter type", "parameter");
            }
            this.Execution.Invoke((T)parameter);
        }
    }
}
