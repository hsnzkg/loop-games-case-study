using System.Collections.Generic;

namespace Project.Scripts.Pool
{
    internal static class PoolManager
    {
        private static readonly List<IPool> s_pools = new List<IPool>();

        public static void Reset()
        {
            for (int i = 0; i < s_pools.Count; i++)
            {
                s_pools[i].Clear();
            }
        }

        public static void Register(IPool pool)
        {
            if (!s_pools.Contains(pool))
            {
                s_pools.Add(pool);
            }
        }
    }
}