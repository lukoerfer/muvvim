using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MvvmUtil.Converter.Special
{
    /// <summary>
    /// Encapsulates the functionality of multiple converters by chaining them in a list
    /// </summary>
    public class ConverterChain : List<IValueConverter>, IValueConverter
    {
        /// <summary>
        /// Converts a value by applying the first chained converter, applying the second one on the result and so on
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="targetType">An ignored target type</param>
        /// <param name="parameter">An ignored parameter object</param>
        /// <param name="culture">The culture to use for each chained converter</param>
        /// <returns>The value resulting from the last conversion</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Apply each converter on result of the previous one, starting with the input value
            return this.Aggregate(value, (current, converter) => converter.Convert(current, typeof(object), null, culture));
        }

        /// <summary>
        /// Converts a value by applying the last chained converter, applying the next-to-last one on the result and so on
        /// </summary>
        /// <param name="value">The input value</param>
        /// <param name="targetType">An ignored target type</param>
        /// <param name="parameter">An ignored parameter object</param>
        /// <param name="culture">The culture to use for each chained converter</param>
        /// <returns>The value resulting from the last conversion (first converter in list)</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Reverse the converter list and apply each converter on the result of the previous one, starting with the input value
            return this.Reverse<IValueConverter>().Aggregate(value, (current, converter) => converter.Convert(current, typeof(object), null, culture));
        }
    }
}
