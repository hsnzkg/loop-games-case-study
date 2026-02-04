using UnityEngine;

namespace Project.Scripts.Utility
{
    public static class LayerMaskUtility
    {
        public static bool Contains(this LayerMask layerMask, int layer)
        {
            return (layerMask.value & (1 << layer)) != 0;
        }
    }
}