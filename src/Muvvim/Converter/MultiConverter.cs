using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Muvvim.Converter
{
    /// <summary>
    /// Provides a simple type-safe multi converter with equal typed inputs
    /// </summary>
    /// <typeparam name="ValueType">The type of each converter input</typeparam>
    /// <typeparam name="TargetType">The converter target type</typeparam>
    public abstract class MultiConverter<ValueType, TargetType> : IMultiValueConverter
    {
        /// <summary>
        /// Provides a type-safe way to implement the multi conversion
        /// </summary>
        /// <param name="values">The collection of input values</param>
        /// <returns>The conversion result</returns>
        protected abstract TargetType Convert(IEnumerable<ValueType> values);

        /// <summary>
        /// Provides a type-safe way to implement the multi back-conversion
        /// </summary>
        /// <param name="value">The back-conversion input value</param>
        /// <returns>The collection of conversion result values</returns>
        protected abstract IEnumerable<ValueType> ConvertBack(TargetType value);

        /// <summary>
        /// Applies the implemented multi conversion and handles the required object casting
        /// </summary>
        /// <param name="values">The multi converter input values</param>
        /// <param name="targetType">A type, will be ignored</param>
        /// <param name="parameter">An object, will be ignored</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The multi conversion result</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(values.Cast<ValueType>());
        }

        /// <summary>
        /// Applies the implemented multi back-conversion and handles the required object casting
        /// </summary>
        /// <param name="value">The multi back-conversion input value</param>
        /// <param name="targetTypes">An array of types, will be ignored</param>
        /// <param name="parameter">An object, will be ignored</param>
        /// <param name="culture">A culture, will be ignored</param>
        /// <returns>The multi back-conversion results</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return ConvertBack((TargetType)value).Cast<object>().ToArray();
        }
    }
}
