using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NetGears.Database.DataAccessLayer
{
    public abstract class GenericDal<TDto> 
        where TDto : class
    {
        private DbSet<TDto> _dbSet { get; }

        protected GenericDal()
        {
            _dbSet = DatabaseFactory.GetNetGearsContext().Set<TDto>();
        }

        public TDto Get(Func<TDto, bool> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IEnumerable<TDto> GetRange(Func<TDto, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public void Add(TDto dto)
        {
            _dbSet.Add(dto);
        }

        public void AddRange(IEnumerable<TDto> dtos)
        {
            _dbSet.AddRange(dtos);
        }

        public void AddRange(params TDto[] dtos)
        {
            _dbSet.AddRange(dtos);
        }

        public void Remove(TDto dto)
        {
            _dbSet.Remove(dto);
        }

        public void RemoveRange(IEnumerable<TDto> dtos)
        {
            _dbSet.RemoveRange(dtos);
        }

        public void RemoveRange(params TDto[] dtos)
        {
            _dbSet.RemoveRange(dtos);
        }
    }
}