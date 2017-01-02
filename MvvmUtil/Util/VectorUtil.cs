using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MvvmUtil.Util
{
    internal static class VectorUtil
    {
        public static Vector Abs(this Vector vector)
        {
            return new Vector(Math.Abs(vector.X), Math.Abs(vector.Y));
        }

    }
}
