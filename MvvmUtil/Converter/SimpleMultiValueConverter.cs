using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    public abstract class SimpleMultiValueConverter<ValueType, TargetType> : IMultiValueConverter
    {
        protected abstract TargetType Convert(IEnumerable<ValueType> values);

        protected abstract IEnumerable<ValueType> ConvertBack(TargetType value);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(values.Cast<ValueType>());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value).Cast<object>().ToArray();
        }
    }
}
