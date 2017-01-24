using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.Converter.Boolean
{
    /// <summary>
    /// Implements the conversion of a collection of boolean values to a single boolean value
    /// </summary>
    public class MultiBooleanConverter : MultiConverter<bool, string, bool>
    {
        private const string AllParameter = "All";
        private const string AnyParameter = "Any";

        /// <summary>
        /// Converts a collection of boolean values to a single boolean value
        /// </summary>
        /// <param name="values">A collection of input values</param>
        /// <param name="parameter">A string containing parameters</param>
        /// <returns>A boolean value depending on the input value and the given parameter</returns>
        public override bool Convert(IEnumerable<bool> values, string parameter)
        {
            if (AllParameter.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
            {
                return values.All(val => val);
            }
            if (AnyParameter.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
            {
                return values.Any(val => val);
            }
            throw new ArgumentException("Given parameter is unknown", nameof(parameter));
        }

        /// <summary>
        /// Back-conversion is not supported
        /// </summary>
        public override IEnumerable<bool> ConvertBack(bool value, string parameter)
        {
            throw new NotSupportedException();
        }
    }
}
