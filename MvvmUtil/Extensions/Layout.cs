using MvvmUtil.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Extensions
{
    public static class Layout
    {
        private const string AutoLength = "auto";
        private const string StarUnit = "*";

        private static string RowsPropertyKey = "Rows";

        public static DependencyProperty RowsProperty = 
            DependencyProperty.RegisterAttached(RowsPropertyKey, typeof(string), typeof(Layout),
                new FrameworkPropertyMetadata(StarUnit, new PropertyChangedCallback(OnRowsChanged)));

        private static string ColumnsPropertyKey = "Columns";

        public static DependencyProperty ColumnsProperty = 
            DependencyProperty.RegisterAttached(ColumnsPropertyKey, typeof(string), typeof(Layout),
                new FrameworkPropertyMetadata(StarUnit, new PropertyChangedCallback(OnColumnsChanged)));

        private static string PositionPropertyKey = "Position";

        public static DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached(PositionPropertyKey, typeof(string), typeof(Layout),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnPositionChanged)));

        public static void SetRows(DependencyObject obj, string value)
        {
            obj.SetValue(RowsProperty, value);
        }

        public static void SetColumns(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        public static void SetPosition(DependencyObject obj, string value)
        {
            obj.SetValue(PositionProperty, value);
        }

        private static void OnRowsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the grid and the definition value
            Grid grid = (Grid)obj;
            string definitions = (string)args.NewValue;
            // Clear previous row definitions
            grid.RowDefinitions.Clear();
            // Add a row definition for each entry
            definitions.Split(Separators.Space, Separators.Comma)
                .Select(def => ParseGridLength(def))
                .Select(length => new RowDefinition() { Height = length })
                .ToList()
                .ForEach(grid.RowDefinitions.Add);
        }

        private static void OnColumnsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // Cast the grid and the definition value
            Grid grid = (Grid)obj;
            string definitions = (string)args.NewValue;
            // Clear previous column definitions
            grid.ColumnDefinitions.Clear();
            // Add a column definition for each entry
            definitions.Split(Separators.Space, Separators.Comma)
                .Select(def => ParseGridLength(def))
                .Select(length => new ColumnDefinition() { Width = length })
                .ToList()
                .ForEach(grid.ColumnDefinitions.Add);
        }

        private static void OnPositionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            UIElement element = (UIElement)obj;
            string value = (string)args.NewValue;
            GridPosition.Parse(value).Apply(element);
        }

        private static GridLength ParseGridLength(string definition)
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

    internal class GridPosition
    {
        private int Row, RowSpan, Column, ColumnSpan;

        private GridPosition() { }

        public static GridPosition Parse(string definition)
        {
            GridPosition position = new GridPosition();
            var values = definition
                .Split(Separators.Space, Separators.Comma)
                .Select(part => part.Split(Separators.Minus)
                    .Select(value => int.Parse(value)));
            position.Row = values.First().First();
            position.RowSpan = values.First().Last() - values.First().First() + 1;
            position.Column = values.Last().First();
            position.ColumnSpan = values.Last().Last() - values.Last().First() + 1;
            return position;
        }

        public void Apply(UIElement element)
        {
            Grid.SetRow(element, this.Row);
            Grid.SetRowSpan(element, this.RowSpan);
            Grid.SetColumn(element, this.Column);
            Grid.SetColumnSpan(element, this.ColumnSpan);
        }
    }
}
