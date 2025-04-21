using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolbox.RenderBase
{
    public static class Vector4Extension
    {
        public static Vector3 Xyz(this Vector4 v) {
            return new Vector3(v.X, v.Y, v.Z);
         }
        public static Vector2 Xy(this Vector4 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static Vector4 ToSRGB(this Vector4 color)
        {
            return new Vector4(
                LinearToSRGB(color.X),
                LinearToSRGB(color.Y),
                LinearToSRGB(color.Z),
                color.W 
            );
        }

        public static Vector4 ToLinear(this Vector4 color)
        {
            return new Vector4(
                SRGBToLinear(color.X),
                SRGBToLinear(color.Y),
                SRGBToLinear(color.Z),
                color.W // Alpha remains unchanged
            );
        }

        private static float SRGBToLinear(float value)
        {
            return (value <= 0.04045f) ? (value / 12.92f) : MathF.Pow((value + 0.055f) / 1.055f, 2.4f);
        }

        private static float LinearToSRGB(float value)
        {
            return (value <= 0.0031308f) ? (12.92f * value) : (1.055f * MathF.Pow(value, 1 / 2.4f) - 0.055f);
        }
    }
}
