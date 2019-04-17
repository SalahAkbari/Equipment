using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentRental.DataAccess
{
    public interface IGenericEfRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Get();
    }
}
