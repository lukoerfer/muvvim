using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Extensions
{
    public static class Grid
    {
        private const string RowsIdentifier = "Rows";
        private const string ColumnsIdentifier = "Columns";

        private const char SpaceSeparator = ' ';
        private const char CommaSeparator = ',';

        private const string AutoLength = "auto";
        private const string StarUnit = "*";

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached(RowsIdentifier, typeof(string), typeof(Grid),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnRowsChanged)));

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached(ColumnsIdentifier, typeof(string), typeof(Grid),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnColumnsChanged)));

        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }

        public static void SetColumns(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        private static void OnRowsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the grid and the definition value
            System.Windows.Controls.Grid grid = (System.Windows.Controls.Grid)obj;
            string definitions = (string)args.NewValue;
            // Clear previous row definitions
            grid.RowDefinitions.Clear();
            // Add a row definition for each entry
            definitions.Split(SpaceSeparator, CommaSeparator)
                .Select(def => Parse(def))
                .Select(length => new RowDefinition() { Height = length })
                .ToList()
                .ForEach(grid.RowDefinitions.Add);
        }

        private static void OnColumnsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the grid and the definition value
            System.Windows.Controls.Grid grid = (System.Windows.Controls.Grid)obj;
            string definitions = (string)args.NewValue;
            // Clear previous column definitions
            grid.ColumnDefinitions.Clear();
            // Add a column definition for each entry
            definitions.Split(SpaceSeparator, CommaSeparator)
                .Select(def => Parse(def))
                .Select(length => new ColumnDefinition() { Width = length })
                .ToList()
                .ForEach(grid.ColumnDefinitions.Add);
        }

        private static GridLength Parse(string definition)
        {
            if (definition.Equals(AutoLength))
            {
                return GridLength.Auto;
            }
            if (definition.EndsWith(StarUnit))
            {
                string value = definition.Substring(0, definition.Length - 1);
                value = string.IsNullOrEmpty(value) ? "1" : value;
                return new GridLength(double.Parse(value), GridUnitType.Star);
            }
            return new GridLength(double.Parse(definition), GridUnitType.Pixel);
        }
    }
}
