using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Command
{
    /// <summary>
    /// Provides a command which invokes an action based on the result of a bool function
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Required by ICommand, but never called
        /// </summary>
        public event EventHandler CanExecuteChanged;

        private readonly Action Execution;
        private readonly Func<bool> Condition;

        /// <summary>
        /// Creates a new RelayCommand
        /// </summary>
        /// <remarks>
        /// The default execution does nothing. The default condition always returns true.
        /// </remarks>
        /// <param name="execution">The action to invoke on execution</param>
        /// <param name="condition">The bool function to invoke for pre-execution check</param>
        public RelayCommand(Action execution = null, Func<bool> condition = null)
        {
            this.Execution = execution ?? (() => { });
            this.Condition = condition ?? (() => true);
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <returns>The result of the condition function</returns>
        public bool CanExecute()
        {
            return this.Condition.Invoke();
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <param name="parameter">Required by ICommand, is ignored</param>
        /// <returns>The result of the condition function</returns>
        public bool CanExecute(object parameter)
        {
            return this.Condition.Invoke();
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        public void Execute()
        {
            this.Execution.Invoke();
        }

        /// <summary>
        /// Invokes the condition function and returns the result
        /// </summary>
        /// <param name="parameter">Required by ICommand, is ignored</param>
        public void Execute(object parameter)
        {
            this.Execution.Invoke();
        }
    }
}
