using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Extensions
{
    /// <summary>
    /// Provides attached properties for input validation
    /// </summary>
    public static class Validation
    {
        /// <summary>
        /// Registers the attached property that handles the validity of an input element
        /// </summary>
        public static DependencyProperty IsValidProperty = 
            DependencyProperty.RegisterAttached("IsValid", typeof(bool), typeof(Validation), 
                new FrameworkPropertyMetadata(false) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Registers the attached property for textbox RegEx patterns
        /// </summary>
        public static DependencyProperty PatternProperty =
            DependencyProperty.RegisterAttached("Pattern", typeof(string), typeof(Validation),
                new FrameworkPropertyMetadata(string.Empty, OnPatternChanged));

        /// <summary>
        /// Sets the validity of an input element
        /// </summary>
        /// <param name="element">The input element</param>
        /// <param name="isValid">Whether the element input value is valid</param>
        public static void SetIsValid(FrameworkElement element, bool isValid)
        {
            element.SetValue(IsValidProperty, isValid);
        }

        /// <summary>
        /// Gets the validity of an input element
        /// </summary>
        /// <param name="element">The input element</param>
        /// <returns>Whether the element input value is valid</returns>
        public static bool GetIsValid(FrameworkElement element)
        {
            return (bool)element.GetValue(IsValidProperty);
        }

        /// <summary>
        /// Sets a RegEx pattern as textbox input validation
        /// </summary>
        /// <param name="textBox">A textbox</param>
        /// <param name="pattern">A RegEx pattern</param>
        public static void SetPattern(TextBox textBox, string pattern)
        {
            textBox.SetValue(PatternProperty, pattern);
        }

        public static string GetPattern(TextBox textBox)
        {
            return (string)textBox.GetValue(PatternProperty);
        }

        private static void OnPatternChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            TextBox textBox = (TextBox)obj;
            string pattern = (string)args.NewValue;
            // Check for a RegEx pattern
            if (pattern != null)
            {
                // Register text changes
                textBox.TextChanged += OnTextChanged;
            }
            else
            {
                // Unregister text changes
                textBox.TextChanged -= OnTextChanged;
            }
            // Start validation
            ValidateViaPattern(textBox);
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            // Start validation
            ValidateViaPattern((TextBox)sender);
        }

        private static void ValidateViaPattern(TextBox textBox)
        {
            // Get the text value
            string input = (string)textBox.GetValue(TextBox.TextProperty);
            // Get the RegEx pattern
            string pattern = (string)textBox.GetValue(PatternProperty);
            // Validate via regular expression
            bool isValid = Regex.IsMatch(input, pattern);
            // Set the validity
            textBox.SetValue(IsValidProperty, isValid);
        }
    }
}
