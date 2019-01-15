using System;
using System.Windows.Input;

namespace Muvvim.Command
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

        private readonly Action<T> Execution;
        private readonly Func<T, bool> Condition;

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
            Execution = execution ?? (arg => { });
            Condition = condition ?? (arg => true);
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <param name="parameter">The generic condition parameter</param>
        /// <returns>The result of the condition function</returns>
        public bool CanExecute(T parameter)
        {
            return Condition.Invoke(parameter);
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
            return Condition.Invoke((T)parameter);
        }

        /// <summary>
        /// Invokes the execution action
        /// </summary>
        /// <param name="parameter">The generic execution parameter</param>
        public void Execute(T parameter)
        {
            Execution.Invoke(parameter);
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
            Execution.Invoke((T)parameter);
        }
    }
}
