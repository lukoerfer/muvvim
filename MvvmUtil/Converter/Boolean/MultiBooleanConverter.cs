using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.Converter.Boolean
{
    public class MultiBooleanConverter : MultiValueConverter<bool, string, bool>
    {
        private const string AllParameter = "All";
        private const string AnyParameter = "Any";

        public override bool Convert(IEnumerable<bool> values, string parameter)
        {
            if (AllParameter.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
            {
                return values.All(val => val);
            }
            if (AnyParameter.Equals(parameter, StringComparison.InvariantCultureIgnoreCase))
            {
                return values.Any(val => val);
            }
            throw new ArgumentException("Given parameter is unknown", "parameter");
        }

        public override IEnumerable<bool> ConvertBack(bool value, string parameter)
        {
            throw new NotSupportedException();
        }
    }
}
