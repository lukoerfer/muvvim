using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvvmUtil.Converter.Boolean
{
    public class BooleanAssignConverter<T> : SimpleValueConverter<bool, T>
    {
        public T TrueObject { get; set; }
        public T FalseObject { get; set; }

        protected override T Convert(bool value)
        {
            return value ? this.TrueObject : this.FalseObject;
        }

        protected override bool ConvertBack(T value)
        {
            if (value.Equals(this.TrueObject)) return true;
            if (value.Equals(this.FalseObject)) return false;
            throw new ArgumentException("Given object is not assignable", "value");
        }
    }
}
