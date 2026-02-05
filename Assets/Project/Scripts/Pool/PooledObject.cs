using System;

namespace Project.Scripts.Pool
{
    public readonly struct PooledObject<T> : IDisposable where T : class
    {
        private readonly T m_Value;
        private readonly IObjectPool<T> m_Pool;

        public PooledObject(T value, IObjectPool<T> pool)
        {
            m_Value = value;
            m_Pool = pool;
        }

        public void Dispose()
        {
            m_Pool.Release(m_Value);
        }
    }
}