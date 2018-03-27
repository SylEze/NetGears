using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetGears.Database.Entities;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace NetGears.Database.Repositories
{
    public abstract class Repository<TDatabaseContext, TEntity> : IRepository<TEntity>
        where TDatabaseContext : DbContext
        where TEntity : class, IEntity
    {
        protected TDatabaseContext _context { get; }

        protected Repository(TDatabaseContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity Get(long id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public TEntity Get(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public int Count(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }
    }
}