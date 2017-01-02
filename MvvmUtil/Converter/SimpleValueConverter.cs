using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    /// <summary>
    /// Provides a simple type-safe base converter class
    /// </summary>
    /// <typeparam name="ValueType">The converter value type</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class SimpleValueConverter<ValueType, TargetType> : IValueConverter
    {
        /// <summary>
        /// Provides a simple way to implement the desired conversion
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>The conversion result</returns>
        protected abstract TargetType Convert(ValueType value);

        /// <summary>
        /// Provides a simple way to implement the desired back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <returns>The back-conversion result</returns>
        protected abstract ValueType ConvertBack(TargetType value);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert((ValueType)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value);
        }

    }
}
