using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmUtil.Extensions
{
    public static class Decoration
    {
        public static DependencyProperty NoIconProperty =
            DependencyProperty.RegisterAttached("NoIcon", typeof(bool), typeof(Decoration));

        public static bool GetNotIcon(Window window)
        {
            return (bool)window.GetValue(NoIconProperty);
        }

        public static void SetNoIcon(Window window, bool value)
        {
            window.SetValue(NoIconProperty, value);
        }

        private static void OnNoIconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue.Equals(true))
            {
                // Remove icon from window
            }
        }
    }
}
