using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    /// <summary>
    /// Provides a type-safe base converter class with parameter support
    /// </summary>
    /// <typeparam name="ValueType">The converter value type</typeparam>
    /// <typeparam name="ParameterType">The converter parameter type</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class ValueConverter<ValueType, ParameterType, TargetType> : IValueConverter
    {
        /// <summary>
        /// Provides a simple way to implement the desired conversion
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="parameter">The parameter value</param>
        /// <returns>The conversion result</returns>
        protected abstract TargetType Convert(ValueType value, ParameterType parameter);

        /// <summary>
        /// Provides a simple way to implement the desired back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="parameter">The parameter value</param>
        /// <returns>The back-conversion result</returns>
        protected abstract ValueType ConvertBack(TargetType value, ParameterType parameter);

        /// <summary>
        /// Applies the implemented conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="targetType">An ignored target type</param>
        /// <param name="parameter">The converter parameter</param>
        /// <param name="culture">An ignored culture</param>
        /// <returns>The conversion result</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Simple cast from object to TargetType / ParameterType
            // Maybe apply type checks for a more precise exception
            return this.Convert((ValueType)value, (ParameterType)parameter);
        }

        /// <summary>
        /// Applies the implemented back-conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">The converter parameter</param>
        /// <param name="culture"></param>
        /// <returns>The back-conversion result</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Simple cast from object to TargetType / ParameterType
            // Maybe apply type checks for a more precise exception
            return this.ConvertBack((TargetType)value, (ParameterType)parameter);
        }
    }
}
