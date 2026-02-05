namespace Project.Scripts.Pool
{
    internal interface IPool
    {
        int GetCountInactive();

        void Clear();
    }
}