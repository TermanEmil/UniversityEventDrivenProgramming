using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public static class Extensions
    {
        public static bool Aprox(this float nb, float otherNb, float epsilon = 0.0001f)
        {
            return Math.Abs(nb - otherNb) < epsilon;
        }

        public static PointF Mult(this PointF point, PointF other)
        {
            return new PointF(point.X * other.X, point.Y * other.Y);
        }

        public static bool Get<T>(this Dictionary<T, bool> dict, T key)
        {
            if (dict.TryGetValue(key, out bool result))
                return result;
            else
                return false;
        }
    }
}
