using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MvvmUtil.Extensions
{
    public static class Mouse
    {
        private const string OnPressedPropertyKey = "OnPressed";

        public static DependencyProperty OnPressedProperty =
            DependencyProperty.RegisterAttached(OnPressedPropertyKey, typeof(ICommand), typeof(Mouse), 
                new FrameworkPropertyMetadata(null, OnPressedChanged));

        private const string OnReleasedPropertyKey = "OnReleased";

        public static DependencyProperty OnReleasedProperty =
            DependencyProperty.RegisterAttached(OnReleasedPropertyKey, typeof(ICommand), typeof(Mouse), 
                new FrameworkPropertyMetadata(null, OnReleasedChanged));

        private const string EnableStatesPropertyKey = "EnableStates";

        public static DependencyProperty EnableStatesProperty =
            DependencyProperty.RegisterAttached(EnableStatesPropertyKey, typeof(bool), typeof(Mouse),
                new FrameworkPropertyMetadata(false, EnableStatesChanged));

        private const string IsPressedPropertyKey = "IsPressed";

        public static DependencyProperty IsPressedProperty =
            DependencyProperty.RegisterAttached(IsPressedPropertyKey, typeof(bool), typeof(Mouse));

        public static void SetOnPressed(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OnPressedProperty, value);
        }

        public static void SetOnReleased(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OnReleasedProperty, value);
        }

        public static void SetEnableStates(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableStatesProperty, value);
        }

        private static void OnPressedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)obj;
            // Check for new value
            if (args.NewValue != null)
            {
                // Register mouse down handler
                element.PreviewMouseDown += OnPreviewMouseDown;
            }
            else
            {
                // Unregister mouse down handler
                element.PreviewMouseDown -= OnPreviewMouseDown;
            }
        }

        private static void OnReleasedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)obj;
            // Check for new value
            if (args.NewValue != null)
            {
                // Register mouse up handler
                element.PreviewMouseUp += OnPreviewMouseUp;
            }
            else
            {
                // Unregister mouse up handler
                element.PreviewMouseUp -= OnPreviewMouseUp;
            }
        }

        private static void EnableStatesChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)obj;
            // Cast the new value
            if ((bool)args.NewValue)
            {
                // Register mouse events
                element.PreviewMouseDown += OnPreviewMouseEvent;
                element.PreviewMouseUp += OnPreviewMouseEvent;
            }
            else
            {
                // Unregister mouse events
                element.PreviewMouseDown -= OnPreviewMouseEvent;
                element.PreviewMouseUp -= OnPreviewMouseEvent;
            }
        }

        private static void OnPreviewMouseDown(object sender, MouseButtonEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)sender;
            // Cast the current handlers
            ICommand pressedHandler = element.GetValue(OnPressedProperty) as ICommand;
            // Execute the pressed handler is possible
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

        private static void OnPreviewMouseEvent(object sender, MouseButtonEventArgs args)
        {
            // Cast the FrameworkElement
            FrameworkElement element = (FrameworkElement)sender;
            // Determine the press state
            bool pressState = new MouseButtonState[] { args.LeftButton, args.MiddleButton,
                args.RightButton, args.XButton1, args.XButton2 }
                    .Any(state => state == MouseButtonState.Pressed);
            // Set the press state value
            element.SetValue(IsPressedProperty, pressState);
        }
    }
}
