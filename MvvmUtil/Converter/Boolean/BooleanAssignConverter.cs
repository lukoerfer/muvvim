using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.Converter.Boolean
{
    public class BooleanAssignConverter : SimpleValueConverter<bool, object>
    {
        public object TrueObject { get; set; }
        public object FalseObject { get; set; }

        protected override object Convert(bool value)
        {
            return value ? this.TrueObject : this.FalseObject;
        }

        protected override bool ConvertBack(object value)
        {
            if (value.Equals(this.TrueObject)) return true;
            if (value.Equals(this.FalseObject)) return false;
            throw new ArgumentException("Given object is not assignable", "value");
        }
    }
}
