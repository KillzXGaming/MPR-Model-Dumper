using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaToolbox.RenderBase
{
    public static class Vector3Extension
    {
        public static Vector2 Project(this Vector3 coord, float width, float height, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix)
        {
            // Transform the world position into clip space
            Vector4 clipSpacePos = Vector4.Transform(new Vector4(coord, 1.0f), viewMatrix * projectionMatrix);

            // Perform perspective division to get normalized device coordinates (NDC)
            if (clipSpacePos.W != 0)
            {
                clipSpacePos /= clipSpacePos.W;
            }

            // Convert NDC (-1 to 1) to screen coordinates (0 to width/height)
            float screenX = (clipSpacePos.X * 0.5f + 0.5f) * width;
            float screenY = (1.0f - (clipSpacePos.Y * 0.5f + 0.5f)) * height; // Flip Y to match screen space

            return new Vector2(screenX, screenY);
        }
        public static bool IsUniform(this Vector3 value)
        {
            return value.X == value.Y && value.Y == value.Z;
        }

        public static Vector3 Snap(this Vector3 value, Vector3 snap)
        {
            if (snap.Length() > 0.0f)
                return new Vector3(
                    MathF.Floor(value.X / snap.X) * snap.X,
                    MathF.Floor(value.Y / snap.Y) * snap.Y,
                    MathF.Floor(value.Z / snap.Y) * snap.Z);
            return value;
        }

        public static Vector3 Snap(this Vector3 value, float snap)
        {
            if (snap > 0.0f)
                return new Vector3(
                    MathF.Floor(value.X / snap) * snap,
                    MathF.Floor(value.Y / snap) * snap,
                    MathF.Floor(value.Z / snap) * snap);
            return value;
        }
    }
}
