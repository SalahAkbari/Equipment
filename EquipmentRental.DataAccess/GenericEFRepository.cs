using EquipmentRental.DataAccess.DbContext;
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
    }
}
