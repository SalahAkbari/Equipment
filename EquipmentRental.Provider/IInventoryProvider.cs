using EquipmentRental.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentRental.Provider
{
    public interface IInventoryProvider
    {
        Task<IEnumerable<InventoryDto>> GetAllInventories();
    }
}
