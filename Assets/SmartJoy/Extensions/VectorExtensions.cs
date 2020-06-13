using UnityEngine;

namespace SmartJoy
{
    public static class VectorExtensions
    {
        public static Vector2 CustomNormalized(this Vector2 vector, float relativeValue = 1f)
        {
            return new Vector2
            (
                Mathf.Abs(vector.x) > relativeValue ? vector.x >= 0f ? 1f : -1f : vector.x / relativeValue,
                Mathf.Abs(vector.y) > relativeValue ? vector.y >= 0f ? 1f : -1f : vector.y / relativeValue
            );
        }

        public static Vector2 CustomNormalized(this Vector2 vector, Vector2 relativeVector)
        {
            return new Vector2
            (
                Mathf.Abs(vector.x) > relativeVector.x ? vector.x >= 0f ? 1f : -1f : vector.x / relativeVector.x,
                Mathf.Abs(vector.y) > relativeVector.y ? vector.y >= 0f ? 1f : -1f : vector.y / relativeVector.y
            );
        }

        public static Vector2 CustomClampMax(this Vector2 vector, float clampValue)
        {
            #if DEBUG
            UnityEngine.Debug.Log($"[VectorExtensions] clamp info vector {vector} clampValue {clampValue}");
            #endif
            
            return new Vector2
            (
                vector.x > clampValue ? clampValue : vector.x,
                vector.y > clampValue ? clampValue : vector.y
            );
        }
    }
}