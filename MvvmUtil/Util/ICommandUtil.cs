using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MvvmUtil.Util
{
    internal static class ICommandUtil
    {
        public static void ExecuteIfPossible(this ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }

    }
}
