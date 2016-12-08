using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MvvmUtil.Converter.Special
{
    public class EnumDescriptionConverter : SimpleValueConverter<Enum, string>
    {
        protected override string Convert(Enum value)
        {
            return value.GetType()
                .GetField(Enum.GetName(value.GetType(), value))
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .Select(attr => attr.Description)
                .FirstOrDefault();
        }

        /// <summary>
        /// Back conversion is not supported
        /// </summary>
        protected override Enum ConvertBack(string value)
        {
            throw new NotSupportedException();
        }
    }
}
