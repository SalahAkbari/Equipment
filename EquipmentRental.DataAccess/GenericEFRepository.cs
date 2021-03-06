﻿using EquipmentRental.DataAccess.DbContext;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentRental.DataAccess
{
    public class GenericEfRepository<TEntity> : IGenericEfRepository<TEntity>
        where TEntity : class
    {
        private readonly SqlDbContext _db;
        public GenericEfRepository(SqlDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            return await Task.FromResult(_db.Set<TEntity>());
        }

        public async Task<TEntity> Get(int id)
        {
            var entity = await Task.FromResult(_db.Set<TEntity>().Find(id));
            return entity;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public void Add(TEntity item)
        {
            _db.Add(item);
        }

        public void Delete(TEntity item)
        {
            _db.Set<TEntity>().Remove(item);
        }
    }
}
