using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    public class ConditionCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> Execution;
        private Func<T, bool> Condition;

        public ConditionCommand(Action<T> execution, Func<T, bool> condition)
        {
            this.Execution = execution;
            this.Condition = condition;
        }

        public bool CanExecute(T parameter)
        {
            return this.Condition.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is T))
            {
                throw new ArgumentException("Wrong parameter type", "parameter");
            }
            return this.Condition.Invoke((T)parameter);
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
