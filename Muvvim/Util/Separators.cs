using System.Linq;

namespace Muvvim.Util
{
    /// <summary>
    /// Defines separator chars for splitting and joining
    /// </summary>
    internal static class Separators
    {
        /// <summary>
        /// Defines the space separator
        /// </summary>
        public const string Space = " ";
        /// <summary>
        /// Defines the comma separator
        /// </summary>
        public const string Comma = ",";
        /// <summary>
        /// Defines the minus separator
        /// </summary>
        public const string Minus = "-";
        /// <summary>
        /// Defines the point separator
        /// </summary>
        public const string Point = ".";

        /// <summary>
        /// Converts a string to a char by using the first character
        /// </summary>
        /// <param name="str">An input string</param>
        /// <returns>The first character of the input string or the 0 char, if the input string is empty</returns>
        public static char AsChar(this string str)
        {
            return str.FirstOrDefault();
        }
    }
}
