using System.Collections.Generic;

namespace Project.Scripts.Pool
{
    internal static class PoolManager
    {
        private static readonly List<IPool> s_Pools = new List<IPool>();

        public static void Reset()
        {
            for (int i = 0; i < s_Pools.Count; i++)
            {
                s_Pools[i].Clear();
            }
        }

        public static void Register(IPool pool)
        {
            if (!s_Pools.Contains(pool))
            {
                s_Pools.Add(pool);
            }
        }
    }
}