using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Util
{
    internal class GridUtil
    {
        public const string AutoLength = "auto";
        public const string StarUnit = "*";

        public static GridLength ParseGridLength(string definition)
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
                .Split(Separators.Space[0], Separators.Comma[0])
                .Select(part => part.Split(Separators.Minus[0])
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
