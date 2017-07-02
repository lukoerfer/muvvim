using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MvvmUtil.Util
{
    /// <summary>
    /// Provides utility methods to handle WPF Grids
    /// </summary>
    internal class GridUtil
    {
        /// <summary>
        /// Defines the identifier to size Grid rows or columns depending on their contents
        /// </summary>
        public const string AutoLength = "auto";
        /// <summary>
        /// Defines the identifier to size Grids rows or columns relatively to each other
        /// </summary>
        public const string StarUnit = "*";

        /// <summary>
        /// Parses a string to a GridLength
        /// </summary>
        /// <remarks>
        /// Allowed values are 'auto' or any double, which can be followed by a star ('*').
        /// All other inputs will cause a parsing exception.
        /// </remarks>
        /// <param name="definition">A grid length definition string</param>
        /// <returns>A GridLength object</returns>
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

    /// <summary>
    /// Stores the values required to define the position of an UIElement in a Grid
    /// </summary>
    internal class GridPosition
    {
        private int Row, RowSpan, Column, ColumnSpan;

        private GridPosition() { }

        /// <summary>
        /// Creates a new GridPosition based on a string that lists the required values
        /// </summary>
        /// <param name="definition">A grid position definition string</param>
        /// <returns>A GridPosition object</returns>
        public static GridPosition Parse(string definition)
        {
            GridPosition position = new GridPosition();
            var values = definition
                .Split(Separators.Space.ToChar(), Separators.Comma.ToChar())
                .Select(part => part.Split(Separators.Minus.ToChar())
                    .Select(value => int.Parse(value)));
            position.Row = values.First().First();
            position.RowSpan = values.First().Last() - values.First().First() + 1;
            position.Column = values.Last().First();
            position.ColumnSpan = values.Last().Last() - values.Last().First() + 1;
            return position;
        }

        /// <summary>
        /// Applies the position values to an UIElement
        /// </summary>
        /// <param name="element">The UIElement to position</param>
        public void Apply(UIElement element)
        {
            Grid.SetRow(element, this.Row);
            Grid.SetRowSpan(element, this.RowSpan);
            Grid.SetColumn(element, this.Column);
            Grid.SetColumnSpan(element, this.ColumnSpan);
        }
    }
}
