using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    /// <summary>
    /// Provides a simple type-safe base converter
    /// </summary>
    /// <typeparam name="ValueType">The converter value type</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class SimpleConverter<ValueType, TargetType> : IValueConverter
    {
        /// <summary>
        /// Provides a type-safe way to implement the conversion
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>The conversion result</returns>
        protected abstract TargetType Convert(ValueType value);

        /// <summary>
        /// Provides a type-safe way to implement the back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <returns>The back-conversion result</returns>
        protected abstract ValueType ConvertBack(TargetType value);

        /// <summary>
        /// Applies the implemented conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="targetType">A type, will be ignored</param>
        /// <param name="parameter">An object, will be ignored</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The conversion result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert((ValueType)value);
        }

        /// <summary>
        /// Applies the implemented back-conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="targetType">A type, will be ignored</param>
        /// <param name="parameter">An object, will be ignored</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The back-conversion result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value);
        }

    }
}
