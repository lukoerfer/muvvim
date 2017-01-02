using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace MvvmUtil.Extensions
{
    public static class Selection
    {
        private const string OnChangePropertyKey = "OnChange";

        public static DependencyProperty OnChangeProperty =
            DependencyProperty.RegisterAttached(OnChangePropertyKey, typeof(ICommand), typeof(Selection),
                new FrameworkPropertyMetadata(null, OnChangeChanged));

        public static void SetOnChange(DependencyObject obj, ICommand value)
        {
            obj.SetValue(OnChangeProperty, value);
        }

        private static void OnChangeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the Selector
            Selector selector = (Selector)obj;
            // Check for a command
            if (args.NewValue != null)
            {
                // Register changed handler
                selector.SelectionChanged += OnSelectionChanged;
            }
            else
            {
                // Unregister changed handler
                selector.SelectionChanged -= OnSelectionChanged;
            }
        }

        private static void OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            // Cast the selector
            Selector selector = (Selector)sender;
            // Cast the current selection handler
            ICommand selectionHandler = selector.GetValue(OnChangeProperty) as ICommand;
            if (selectionHandler == null) return;
            // Execute the selection handler if possible
            if (selectionHandler.CanExecute(selector.SelectedItem))
            {
                selectionHandler.Execute(selector.SelectedItem);
            }
        }
    }
}
