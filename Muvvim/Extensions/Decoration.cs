using System;
using System.Windows;

using Muvvim.Util;

namespace Muvvim.Extensions
{
    /// <summary>
    /// Provides attached properties to control WPF window decorations
    /// </summary>
    public static class Decoration
    {
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty NoIconProperty =
            DependencyProperty.RegisterAttached("NoIcon", typeof(bool), typeof(Decoration), 
                new FrameworkPropertyMetadata(OnNoIconChanged));

        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty NoCloseProperty =
            DependencyProperty.RegisterAttached("NoClose", typeof(bool), typeof(Decoration),
                new FrameworkPropertyMetadata(OnNoCloseChanged));

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static bool GetNotIcon(Window window)
        {
            return (bool)window.GetValue(NoIconProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="window"></param>
        /// <param name="value"></param>
        public static void SetNoIcon(Window window, bool value)
        {
            window.SetValue(NoIconProperty, value);
        }

        private static void OnNoIconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Window window = (Window)obj;

        }

        private static void OnNoCloseChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Window window = (Window)obj;
            
        }

        private static void RunAction(Window window, Action action)
        {
            if (window.IsInitialized)
            {
                action.Invoke();
            }
            else
            {
                window.SourceInitialized += (sender, args) => { action.Invoke(); };
            }
        }

    }
}
