using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MvvmUtil.Extensions
{
    public static class Validation
    {
        public static DependencyProperty IsValidProperty = 
            DependencyProperty.RegisterAttached("IsValid", typeof(bool), typeof(Validation), 
                new FrameworkPropertyMetadata(false) { BindsTwoWayByDefault = true });

        public static DependencyProperty PatternProperty =
            DependencyProperty.RegisterAttached("Pattern", typeof(string), typeof(Validation),
                new FrameworkPropertyMetadata(string.Empty, OnPatternChanged));

        public static void SetIsValid(DependencyObject obj, bool value)
        {
            obj.SetValue(IsValidProperty, value);
        }

        public static bool GetIsValid(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsValidProperty);
        }

        public static void SetPattern(DependencyObject obj, string value)
        {
            obj.SetValue(PatternProperty, value);
        }

        private static void OnPatternChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textBox = (TextBox)obj;
            string pattern = (string)args.NewValue;
            if (pattern != null)
            {
                textBox.TextChanged += OnTextChanged;
                // Start validation
                Validate(textBox);
            }
            else
            {
                textBox.TextChanged -= OnTextChanged;
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            // Start validation
            Validate((TextBox)sender);
        }

        private static void Validate(TextBox textBox)
        {
            string input = (string)textBox.GetValue(TextBox.TextProperty);
            string pattern = (string)textBox.GetValue(PatternProperty);
            // Validate via regular expression
            bool isValid = Regex.IsMatch(input, pattern);
            textBox.SetValue(IsValidProperty, isValid);
        }
    }
}
