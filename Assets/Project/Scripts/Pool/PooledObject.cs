using System;

namespace Project.Scripts.Pool
{
    public readonly struct PooledObject<T> : IDisposable where T : class
    {
        private readonly T m_value;
        private readonly IObjectPool<T> m_pool;

        public PooledObject(T value, IObjectPool<T> pool)
        {
            m_value = value;
            m_pool = pool;
        }

        public void Dispose()
        {
            m_pool.Release(m_value);
        }
    }
}