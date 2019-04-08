
using System.Linq;
using Sisvon.BO.Interfaces;
using Sisvon.Infra.Data.Context;
using Sisvon.Infra.Data.Repositories;
using Sisvon.Model.Interfaces.Repositories;

namespace Sisvon.BO
{
    public class BoBase<TEntity> : IBoBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;
        protected readonly SisvonContext _Context;

        public BoBase(SisvonContext context)
        {
            _Context = context;
            _repository = new RepositoryBase<TEntity>(context);
        }

        public virtual void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public virtual void Remove(TEntity obj)
        {
            _repository.Delete(obj);
        }

    }
}
