namespace Project.Scripts.Pool
{
    public interface IObjectPool<T> where T : class
    {
        int GetCountInactive();

        T Get();

        PooledObject<T> Get(out T v);

        void Release(T element);

        void Clear();
    }
}