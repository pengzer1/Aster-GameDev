namespace AG.GameLogic.ObjectPooling
{
    public interface IObjectPool
    {
        PoolableObject GetObjectFromPool();

        void InsertObjectToPool(PoolableObject obj);

        void SupplyObjectPool();

        void ReturnAllObjects();
    }
}