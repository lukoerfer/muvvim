using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    /// <summary>
    /// Provides a command which invokes a generic action based on the result of a generic bool function
    /// </summary>
    /// <typeparam name="T">The action and func argument type</typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// Required by ICommand, but never called
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private Action<T> Execution;
        private Func<T, bool> Condition;

        /// <summary>
        /// Creates a new RelayCommand
        /// </summary>
        /// <remarks>
        /// The default execution does nothing. The default condition always returns true.
        /// </remarks>
        /// <param name="execution">The action to invoke on execution</param>
        /// <param name="condition">The bool function to invoke for pre-execution check</param>
        public RelayCommand(Action<T> execution = null, Func<T, bool> condition = null)
        {
            this.Execution = execution ?? ((arg) => { });
            this.Condition = condition ?? ((arg) => { return true; });
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <param name="parameter">The generic condition parameter</param>
        /// <returns>The result of the condition function</returns>
        public bool CanExecute(T parameter)
        {
            return this.Condition.Invoke(parameter);
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <param name="parameter">The condition parameter, must be of parameter type</param>
        /// <returns>The result of the condition function</returns>
        public bool CanExecute(object parameter)
        {
            // Check the parameter type
            if (!(parameter is T))
            {
                throw new ArgumentException("Wrong parameter type", nameof(parameter));
            }
            return this.Condition.Invoke((T)parameter);
        }

        /// <summary>
        /// Invokes the execution action
        /// </summary>
        /// <param name="parameter">The generic execution parameter</param>
        public void Execute(T parameter)
        {
            this.Execution.Invoke(parameter);
        }

        /// <summary>
        /// Invokes the execution action
        /// </summary>
        /// <param name="parameter">The execution parameter, must be of parameter type</param>
        public void Execute(object parameter)
        {
            // Check the parameter type
            if (!(parameter is T))
            {
                throw new ArgumentException("Wrong parameter type", nameof(parameter));
            }
            this.Execution.Invoke((T)parameter);
        }
    }
}
