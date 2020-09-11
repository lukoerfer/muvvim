using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Muvvim.Converter
{
    /// <summary>
    /// Provides a type-safe base multi converter with equal typed inputs and parameter support
    /// </summary>
    /// <typeparam name="ValueType">The type of each converter input</typeparam>
    /// <typeparam name="ParameterType">The converter parameter type</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class MultiConverter<ValueType, ParameterType, TargetType> : IMultiValueConverter
    {
        /// <summary>
        /// Provides a type-safe way to implement the multi conversion
        /// </summary>
        /// <param name="values">The collection of input values</param>
        /// <param name="parameter">The converter parameter</param>
        /// <returns>The conversion result</returns>
        public abstract TargetType Convert(IEnumerable<ValueType> values, ParameterType parameter);

        /// <summary>
        /// Provides a type-safe way to implement the multi back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <param name="parameter">The converter parameter</param>
        /// <returns>The collection of back-conversion result values</returns>
        public abstract IEnumerable<ValueType> ConvertBack(TargetType value, ParameterType parameter);

        /// <summary>
        /// Applies the implemented multi conversion and handles the required object casting
        /// </summary>
        /// <param name="values">The multi converter input values</param>
        /// <param name="targetType">A type, will be ignored</param>
        /// <param name="parameter">The converter parameter object</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The multi conversion result</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(values.Cast<ValueType>(), (ParameterType)parameter);
        }

        /// <summary>
        /// Applies the implemented multi back-conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The multi back-conversion input value</param>
        /// <param name="targetTypes">An array of types, will be ignored</param>
        /// <param name="parameter">The converter parameter object</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The multi back-conversion results</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ConvertBack((TargetType)value, (ParameterType)parameter).Cast<object>().ToArray();
        }
    }
}
