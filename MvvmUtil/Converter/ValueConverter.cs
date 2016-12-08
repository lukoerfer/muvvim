using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    public abstract class ValueConverter<ValueType, ParameterType, TargetType> : IValueConverter
    {
        public abstract TargetType Convert(ValueType value, ParameterType parameter);

        public abstract ValueType ConvertBack(TargetType value, ParameterType parameter);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert((ValueType)value, (ParameterType)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value, (ParameterType)parameter);
        }
    }
}
