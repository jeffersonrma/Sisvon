using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sisvon.Model.Interfaces.Repositories;
using Sisvon.Infra.Data.Context;

namespace Sisvon.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        public SisvonContext Db;

        public RepositoryBase(SisvonContext db)
        {
            Db = db;
        }
        public void Add(TEntity obj)
        {
            Db.Set<TEntity>()
                .Add(obj);
            Db.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>()
                .Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Db.Set<TEntity>();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj)
                .State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Delete(TEntity obj)
        {
            Db.Set<TEntity>()
                .Remove(obj);
            Db.SaveChanges();
        }

        public void Dispose()
        {

        }

    }
}
