namespace MvvmUtil.Converter.Boolean
{
    /// <summary>
    /// 
    /// </summary>
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
