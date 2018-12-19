using System.Windows;

namespace MvvmUtil.Converter.Special
{
    /// <summary>
    /// Implements the conversion from a boolean value to a visibility
    /// </summary>
    public class BooleanToVisibilityConverter : Converter<bool, string, Visibility>
    {
        private const string InverseParameter = "Inverse";
        private const string CollapseParameter = "Collapse";

        /// <summary>
        /// Converts a boolean to a visibility
        /// </summary>
        /// <param name="value">The input boolean value</param>
        /// <param name="parameters">A string containing parameters</param>
        /// <returns>A visibility, depending on the input boolean and possible parameters</returns>
        protected override Visibility Convert(bool value, string parameters)
        {
            return InverseIfRequested(value, parameters) ? Visibility.Visible : DetermineRequestedInvisibility(parameters);
        }

        /// <summary>
        /// Converts a visibility to a boolean
        /// </summary>
        /// <param name="value">A visibility</param>
        /// <param name="parameters">A string containing parameters</param>
        /// <returns>A boolean value, depending on the input visibility and possible parameter</returns>
        protected override bool ConvertBack(Visibility value, string parameters)
        {
            return InverseIfRequested(value == Visibility.Visible, parameters);
        }

        private static bool InverseIfRequested(bool value, string parameters)
        {
            return value ^ parameters.Contains(InverseParameter);
        }

        private static Visibility DetermineRequestedInvisibility(string parameters)
        {
            return parameters.Contains(CollapseParameter) ? Visibility.Collapsed : Visibility.Hidden;
        }
    }
}
