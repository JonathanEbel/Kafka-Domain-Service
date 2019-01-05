
namespace Core
{
    public interface IRepository<TEntity, in TKey> where TEntity : class
    {
        TEntity Get(TKey id);
        void Add(TEntity entity);
        void Save();
        void Delete(TEntity entity);
    }
}
