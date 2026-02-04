using UnityEngine;

namespace Project.Scripts.Utility
{
    public static class VectorUtility
    {
        public static Vector2 ClampByUnit(this Vector2 value)
        {
            return Vector2.ClampMagnitude(value, 1f);
        }

        public static Vector3 ClampByUnit(this Vector3 value)
        {
            return Vector3.ClampMagnitude(value, 1f);
        }

        public static Vector3 ToVector3XZ(this Vector2 value)
        {
            return new Vector3(value.x, 0f, value.y);
        }

        public static Vector2 ToVector2XY(this Vector3 value)
        {
            return new Vector2(value.x,value.y);
        }
    }
}