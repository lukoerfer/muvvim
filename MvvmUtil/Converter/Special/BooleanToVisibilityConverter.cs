using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmUtil.Converter.Special
{
    public class BooleanToVisibilityConverter : ValueConverter<bool, string, Visibility>
    {
        public override Visibility Convert(bool value, string parameter)
        {
            return InverseOnParameter(value, parameter) ? Visibility.Visible : GetInvisibility(parameter);
        }

        public override bool ConvertBack(Visibility value, string parameter)
        {
            return InverseOnParameter(value == Visibility.Visible, parameter);
        }

        #region | Parameters |

        private const string InverseParameter = "inverse";
        private const string CollapseParameter = "collapse";

        private static bool InverseOnParameter(bool value, string parameter)
        {
            return value ^ parameter.Contains(InverseParameter);
        }

        private static Visibility GetInvisibility(string parameter)
        {
            return parameter.Contains(CollapseParameter) ? Visibility.Collapsed : Visibility.Hidden;
        }

        #endregion // Parameters
    }
}
