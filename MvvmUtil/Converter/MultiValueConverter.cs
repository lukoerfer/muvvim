using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    public abstract class MultiValueConverter<ValueType, ParameterType, TargetType> : IMultiValueConverter
    {
        public abstract TargetType Convert(IEnumerable<ValueType> values, ParameterType parameter);

        public abstract IEnumerable<ValueType> ConvertBack(TargetType value, ParameterType parameter);

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(values.Cast<ValueType>(), (ParameterType)parameter);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value, (ParameterType)parameter).Cast<object>().ToArray();
        }
    }
}
