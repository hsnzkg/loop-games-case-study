using System;
using System.Collections.Generic;

namespace Project.Scripts.Pool
{
    public class ObjectPool<T> : IDisposable, IPool, IObjectPool<T> where T : class
    {
        internal readonly List<T> List;
        internal readonly bool CollectionCheck;
        private readonly Func<T> m_CreateFunc;
        private readonly Action<T> m_ActionOnGet;
        private readonly Action<T> m_ActionOnRelease;
        private readonly Action<T> m_ActionOnDestroy;
        private readonly int m_MaxSize;
        private int m_CountAll;

        private int GetCountAll()
        {
            return m_CountAll;
        }

        private void SetCountAll(int value)
        {
            m_CountAll = value;
        }

        public int GetCountActive()
        {
            return GetCountAll() - GetCountInactive();
        }

        public int GetCountInactive()
        {
            return List.Count;
        }

        public ObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            if (maxSize <= 0)
            {
                throw new ArgumentException("Max Size must be greater than 0", nameof (maxSize));
            }
            List = new List<T>(defaultCapacity);
            m_CreateFunc = createFunc ?? throw new ArgumentNullException(nameof (createFunc));
            m_MaxSize = maxSize;
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
            m_ActionOnDestroy = actionOnDestroy;
            CollectionCheck = collectionCheck;
            PoolManager.Register(this);
        }

        public T Get()
        {
            T obj;
            if (List.Count == 0)
            {
                obj = m_CreateFunc();
                SetCountAll(GetCountAll() + 1);
            }
            else
            {
                int index = List.Count - 1;
                obj = List[index];
                List.RemoveAt(index);
            }
            Action<T> actionOnGet = m_ActionOnGet;
            if (actionOnGet != null)
            {
                actionOnGet(obj);
            }
            return obj;
        }

        public PooledObject<T> Get(out T v)
        {
            return new PooledObject<T>(v = Get(), this);
        }

        public void Release(T element)
        {
            if (CollectionCheck && List.Count > 0)
            {
                for (int index = 0; index < List.Count; ++index)
                {
                    if (element == List[index])
                    {
                        throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
                    }
                }
            }
            Action<T> actionOnRelease = m_ActionOnRelease;
            if (actionOnRelease != null)
            {
                actionOnRelease(element);
            }
            if (GetCountInactive() < m_MaxSize)
            {
                List.Add(element);
            }
            else
            {
                SetCountAll(GetCountAll() - 1);
                Action<T> actionOnDestroy = m_ActionOnDestroy;
                if (actionOnDestroy != null)
                {
                    actionOnDestroy(element);
                }
            }
        }

        public void Clear()
        {
            if (m_ActionOnDestroy != null)
            {
                foreach (T obj in List)
                {
                    m_ActionOnDestroy(obj);
                }
            }
            List.Clear();
            SetCountAll(0);
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
