using System;
using System.Globalization;
using System.Windows.Data;

namespace Muvvim.Converter
{
    /// <summary>
    /// Provides a type-safe base converter with parameter support
    /// </summary>
    /// <typeparam name="ValueType">The converter value type</typeparam>
    /// <typeparam name="ParameterType">The converter parameter type</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class Converter<ValueType, ParameterType, TargetType> : IValueConverter
    {
        /// <summary>
        /// Provides a type-safe way to implement the conversion
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="parameter">The parameter value</param>
        /// <returns>The conversion result</returns>
        protected abstract TargetType Convert(ValueType value, ParameterType parameter);

        /// <summary>
        /// Provides a type-safe way to implement the back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="parameter">The parameter value</param>
        /// <returns>The back-conversion result</returns>
        protected abstract ValueType ConvertBack(TargetType value, ParameterType parameter);

        /// <summary>
        /// Applies the implemented conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="targetType">A type, will be ignored</param>
        /// <param name="parameter">The converter parameter object</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The conversion result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Simple cast from object to TargetType / ParameterType
            // Maybe apply type checks for a more precise exception
            return Convert((ValueType)value, (ParameterType)parameter);
        }

        /// <summary>
        /// Applies the implemented back-conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="targetType">A target type, will be ignored</param>
        /// <param name="parameter">The converter parameter object</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The back-conversion result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Simple cast from object to TargetType / ParameterType
            // Maybe apply type checks for a more precise exception
            return ConvertBack((TargetType)value, (ParameterType)parameter);
        }
    }
}
