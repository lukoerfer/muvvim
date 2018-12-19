using System;

namespace MvvmUtil.Converter.Boolean
{
    /// <summary>
    /// Implements the conversion of a boolean value to one of two preassigned target objects
    /// </summary>
    /// <typeparam name="T">The type of the target object</typeparam>
    public class BooleanAssignConverter<T> : Converter<bool, T>
    {
        /// <summary>
        /// Gets or sets the target object to return if the input value is true
        /// </summary>
        public T TrueObject { get; set; }
        /// <summary>
        /// Gets or sets the target object to return if the input value is false
        /// </summary>
        public T FalseObject { get; set; }

        /// <summary>
        /// Converts a boolean value to one of the two preassigned target objects
        /// </summary>
        /// <param name="value">A boolean value</param>
        /// <returns>The TrueObject, if the input value is true, the FalseObject if the input value is false</returns>
        protected override T Convert(bool value)
        {
            return value ? TrueObject : FalseObject;
        }

        /// <summary>
        /// Converts an object to a boolean value, depending on the equality to the preassigned target objects
        /// </summary>
        /// <param name="value">Either the TrueObject or the FalseObject</param>
        /// <returns>True, if the input object equals the TrueObject, false, if the input object equals the FalseObject</returns>
        protected override bool ConvertBack(T value)
        {
            if (value.Equals(TrueObject)) return true;
            if (value.Equals(FalseObject)) return false;
            throw new ArgumentException("Given object is not assignable", "value");
        }
    }
}
