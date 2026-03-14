using UnityEngine;

namespace Smoove.Utilities
{
    /// <summary>
    /// Commonly used extension methods across the project.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>Returns true if the layer is included in the LayerMask.</summary>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }

        /// <summary>Remaps a value from one range to another.</summary>
        public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }

        /// <summary>Returns a Vector3 with the Y component set to zero.</summary>
        public static Vector3 Flat(this Vector3 v)
        {
            return new Vector3(v.x, 0f, v.z);
        }

        /// <summary>Returns the direction from this transform to a target position.</summary>
        public static Vector3 DirectionTo(this Transform from, Vector3 target)
        {
            return (target - from.position).normalized;
        }

        /// <summary>Attempts to get a component; logs an error if not found.</summary>
        public static bool TryGetComponentWithLog<T>(this GameObject go, out T component) where T : Component
        {
            component = go.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"[ExtensionMethods] {typeof(T).Name} not found on {go.name}.");
                return false;
            }
            return true;
        }
    }
}
