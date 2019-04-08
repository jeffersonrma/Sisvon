
using System.Collections.Generic;
using System.Linq;

namespace Sisvon.BO.Interfaces
{
    public interface IBoBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);

    }
}
