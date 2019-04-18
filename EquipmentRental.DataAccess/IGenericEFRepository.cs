using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentRental.DataAccess
{
    public interface IGenericEfRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(int id);
        bool Save();
        void Add(TEntity item);
        void Delete(TEntity item);
    }
}
