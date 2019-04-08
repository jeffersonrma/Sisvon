

using System.Linq;

namespace Sisvon.Model.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Delete(TEntity obj);
        void Dispose();


    }
}
