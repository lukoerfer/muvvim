using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Muvvim.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Closing
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TriggerProperty =
            DependencyProperty.RegisterAttached("Trigger", typeof(bool), typeof(Closing),
                new FrameworkPropertyMetadata(OnTriggerChanged));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static bool GetTrigger(DependencyObject window)
        {
            return (bool)window.GetValue(TriggerProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="value"></param>
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
