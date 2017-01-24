namespace MvvmUtil.Converter.Boolean
{
    /// <summary>
    /// Implements the conversion of a boolean value to its opposite
    /// </summary>
    public sealed class BooleanOppositeConverter : SimpleConverter<bool, bool>
    {
        /// <summary>
        /// Converts a boolean value to its opposite
        /// </summary>
        /// <param name="value">A boolean value</param>
        /// <returns>The opposite of the input value</returns>
        protected sealed override bool Convert(bool value)
        {
            return !value;
        }

        /// <summary>
        /// Converts a boolean value to its opposite
        /// </summary>
        /// <param name="value">A boolean value</param>
        /// <returns>The opposite of the input value</returns>
        protected sealed override bool ConvertBack(bool value)
        {
            return !value;
        }
    }
}
