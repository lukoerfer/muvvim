using System;
using System.ComponentModel;
using System.Linq;

namespace Muvvim.Converter.Enum
{
    /// <summary>
    /// Implements the conversion of an enum value to its description attribute value
    /// </summary>
    public class EnumDescriptionConverter : Converter<System.Enum, string>
    {
        /// <summary>
        /// Converts an enum value to its description attribute value
        /// </summary>
        /// <param name="value">The input enum value</param>
        /// <returns>The associated description attribute value or null, if no description attribute is set</returns>
        protected sealed override string Convert(System.Enum value)
        {
            return value.GetType()
                .GetField(System.Enum.GetName(value.GetType(), value))
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .Select(attr => attr.Description)
                .FirstOrDefault();
        }

        /// <summary>
        /// Back conversion is not supported
        /// </summary>
        protected sealed override System.Enum ConvertBack(string value)
        {
            throw new NotSupportedException();
        }
    }
}
