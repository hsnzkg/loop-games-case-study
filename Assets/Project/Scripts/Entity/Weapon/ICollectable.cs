namespace Project.Scripts.Entity.Weapon
{
    interface ICollectable
    {
        void Collect();
        bool GetIsCollecting();
        void SetIsCollecting(bool value);
    }
}