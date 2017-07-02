using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MvvmUtil.Extensions
{
    /// <summary>
    /// Provides attached properties to receive mouse events via commands
    /// </summary>
    public static class Mouse
    {
        /// <summary>
        /// Registers the attached property to execute a command on mouse down events
        /// </summary>
        public static DependencyProperty OnPressedProperty =
            DependencyProperty.RegisterAttached("OnPressed", typeof(ICommand), typeof(Mouse),
                new FrameworkPropertyMetadata(null, OnPressedChanged));

        /// <summary>
        /// Registers the attached property to execute a command on mouse up events
        /// </summary>
        public static DependencyProperty OnReleasedProperty =
            DependencyProperty.RegisterAttached("OnReleased", typeof(ICommand), typeof(Mouse),
                new FrameworkPropertyMetadata(null, OnReleasedChanged));

        /// <summary>
        /// Registers the attached property to execute a command on mouse wheel events
        /// </summary>
        public static DependencyProperty OnWheelProperty =
            DependencyProperty.RegisterAttached("OnWheel", typeof(ICommand), typeof(Mouse),
                new FrameworkPropertyMetadata(null, OnWheelChanged));

        /// <summary>
        /// Sets the command to execute on mouse down events
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="command">The command to execute on mouse down events</param>
        public static void SetOnPressed(FrameworkElement element, ICommand command)
        {
            element.SetValue(OnPressedProperty, command);
        }

        public static ICommand GetOnPressed(FrameworkElement element)
        {
            return (ICommand)element.GetValue(OnPressedProperty);
        }

        /// <summary>
        /// Sets the command to execute on mouse up events
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="command">The command to execute on mouse up events</param>
        public static void SetOnReleased(FrameworkElement element, ICommand command)
        {
            element.SetValue(OnReleasedProperty, command);
        }

        public static ICommand GetOnReleased(FrameworkElement element)
        {
            return (ICommand)element.GetValue(OnReleasedProperty);
        }

        /// <summary>
        /// Sets the command to execute on mouse wheel events
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="command">The command to execute on mouse wheel events</param>
        public static void SetOnWheel(FrameworkElement element, ICommand command)
        {
            element.SetValue(OnWheelProperty, command);
        }

        /// <summary>
        /// Gets the command to execute on mouse wheel events
        /// </summary>
        /// <param name="element">The element</param>
        /// <returns>The command to execute on mouse wheel events or null, if it does not exist</returns>
        public static ICommand GetOnWheel(FrameworkElement element)
        {
            return (ICommand)element.GetValue(OnWheelProperty);
        }

        private static void OnPressedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            // Check for new value
            if (args.NewValue != null)
            {
                // Register mouse down event
                element.PreviewMouseDown += OnPreviewMouseDown;
            }
            else
            {
                // Unregister mouse down event
                element.PreviewMouseDown -= OnPreviewMouseDown;
            }
        }

        private static void OnReleasedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            // Check for new value
            if (args.NewValue != null)
            {
                // Register mouse up event
                element.PreviewMouseUp += OnPreviewMouseUp;
            }
            else
            {
                // Unregister mouse up event
                element.PreviewMouseUp -= OnPreviewMouseUp;
            }
        }

        private static void OnWheelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            // Check for new value
            if (args.NewValue != null)
            {
                // Register mouse wheel event
                element.PreviewMouseWheel += OnPreviewMouseWheel;
            }
            else
            {
                // Unregister mouse wheel event
                element.PreviewMouseWheel -= OnPreviewMouseWheel;
            }
        }

        private static void OnPreviewMouseDown(object sender, MouseButtonEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)sender;
            // Cast the current handlers
            ICommand pressedHandler = element.GetValue(OnPressedProperty) as ICommand;
            // Execute the pressed handler if possible
            if (pressedHandler != null && pressedHandler.CanExecute(args.ChangedButton))
            {
                pressedHandler.Execute(args.ChangedButton);
            }
        }

        private static void OnPreviewMouseUp(object sender, MouseButtonEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)sender;
            // Cast the current released handler
            ICommand releasedHandler = element.GetValue(OnReleasedProperty) as ICommand;
            // Execute the released handler if possible
            if (releasedHandler != null && releasedHandler.CanExecute(args.ChangedButton))
            {
                releasedHandler.Execute(args.ChangedButton);
            }
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)sender;
            // Cast the current released handler
            ICommand wheelHandler = element.GetValue(OnWheelProperty) as ICommand;
            // Execute the released handler if possible
            if (wheelHandler != null && wheelHandler.CanExecute(args.Delta))
            {
                wheelHandler.Execute(args.Delta);
            }
        }
    }
}
