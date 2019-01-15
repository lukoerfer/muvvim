using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Muvvim.Extensions
{
    /// <summary>
    /// Provides attached properties for element states
    /// </summary>
    public static class States
    {
        /// <summary>
        /// Registers the attached property to enable element states
        /// </summary>
        public static DependencyProperty EnableProperty =
            DependencyProperty.RegisterAttached("Enable", typeof(bool), typeof(States),
                new FrameworkPropertyMetadata(false, EnableChanged));

        /// <summary>
        /// Registers the attached read-only property indicating whether the element is pressed
        /// </summary>
        public static DependencyPropertyKey IsPressedPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("IsPressed", typeof(bool), typeof(States),
                new FrameworkPropertyMetadata(false));

        // Access to the read-only IsPressed property
        private static readonly DependencyProperty IsPressedProperty = IsPressedPropertyKey.DependencyProperty;

        /// <summary>
        /// Enables or disables states for an element
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">Whether to enable or disable states</param>
        public static void SetEnable(FrameworkElement element, bool value)
        {
            element.SetValue(EnableProperty, value);
        }

        public static bool GetEnable(FrameworkElement element)
        {
            return (bool)element.GetValue(EnableProperty);
        }

        public static bool GetIsPressed(FrameworkElement element)
        {
            return (bool)element.GetValue(IsPressedProperty);
        }

        private static void EnableChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)obj;
            // Check whether to enable states
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

        private static void OnPreviewMouseEvent(object sender, MouseButtonEventArgs args)
        {
            FrameworkElement element = (FrameworkElement)sender;
            // Determine the press state
            bool isPressed = new MouseButtonState[] { args.LeftButton,
                args.MiddleButton, args.RightButton, args.XButton1, args.XButton2 }
                    .Any(state => state == MouseButtonState.Pressed);
            // Set the press state value
            element.SetValue(IsPressedProperty, isPressed);
        }
    }
}
