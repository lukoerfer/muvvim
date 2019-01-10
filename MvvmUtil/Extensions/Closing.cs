using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmUtil.Extensions
{
    public static class Closing
    {
        public static readonly DependencyProperty TriggerProperty =
            DependencyProperty.RegisterAttached("Trigger", typeof(bool), typeof(DependencyObject),
                new FrameworkPropertyMetadata(OnTriggerChanged));

        public static bool GetTrigger(DependencyObject window)
        {
            return (bool)window.GetValue(TriggerProperty);
        }

        public static void SetTrigger(DependencyObject window, bool value)
        {
            window.SetValue(TriggerProperty, value);
        }

        private static void OnTriggerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue.Equals(true))
            {
                Window.GetWindow(obj).Close();
            }
        }
    }
}
