using System;
using System.Windows;

namespace Muvvim.Util
{
    /// <summary>
    /// Implements extension and helper methods for System.Windows.Vector
    /// </summary>
    internal static class VectorUtil
    {
        /// <summary>
        /// Calculates the absolute of a given vector
        /// </summary>
        /// <param name="vector">The source vector</param>
        /// <returns>A new vector with the absolute value for each component</returns>
        public static Vector Abs(this Vector vector)
        {
            return new Vector(Math.Abs(vector.X), Math.Abs(vector.Y));
        }

    }
}
