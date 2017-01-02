using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    public class ToggleCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool IsActive;

        private Action Execution;

        public ToggleCommand(Action execution)
        {
            this.Execution = execution;
        }

        public void SetActive(bool active)
        {
            if (this.IsActive != active)
            {
                this.IsActive = active;
                this.CanExecuteChanged.Invoke(this, EventArgs.Empty);
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

        public bool CanExecute(object parameter)
        {
            return this.IsActive;
        }

        public void Execute()
        {
            this.Execution.Invoke();
        }

        public void Execute(object parameter)
        {
            this.Execution.Invoke();
        }
    }
}
