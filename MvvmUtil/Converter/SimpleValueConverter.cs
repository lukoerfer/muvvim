using System;
using System.Globalization;
using System.Windows.Data;

namespace MvvmUtil.Converter
{
    public abstract class SimpleValueConverter<ValueType, TargetType> : IValueConverter
    {
        protected abstract TargetType Convert(ValueType value);

        protected abstract ValueType ConvertBack(TargetType value);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert((ValueType)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.ConvertBack((TargetType)value);
        }

    }
}
