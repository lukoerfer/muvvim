using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MvvmUtil.Extensions
{
    public static class Input
    {
        private const string OnEditPropertyKey = "OnEdit";

        public static DependencyProperty OnEditProperty =
            DependencyProperty.RegisterAttached(OnEditPropertyKey, typeof(ICommand), typeof(Input),
                new FrameworkPropertyMetadata(null, OnEditChanged));

        public static void SetOnEdit(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OnEditProperty, value);
        }

        private static void OnEditChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the TextBox
            TextBox textbox = (TextBox)obj;
            // Check for a command
            if (args.NewValue != null)
            {
                // Register text changed handler
                textbox.TextChanged += OnTextChanged;
            }
            else
            {
                // Unregister text changed handler
                textbox.TextChanged -= OnTextChanged;
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            // Cast the textbox
            TextBox textbox = (TextBox)sender;
            // Cast the current text handler
            ICommand textHandler = textbox.GetValue(OnEditProperty) as ICommand;
            if (textHandler == null) return;
            // Execute the text handler if possible
            if (textHandler.CanExecute(textbox.Text))
            {
                textHandler.Execute(textbox.Text);
            }
        }
    }
}
