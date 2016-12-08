namespace MvvmUtil.Converter.Boolean
{
    public class InverseBooleanConverter : SimpleValueConverter<bool, bool>
    {
        protected override bool Convert(bool value)
        {
            return !value;
        }

        protected override bool ConvertBack(bool value)
        {
            return !value;
        }
    }
}
