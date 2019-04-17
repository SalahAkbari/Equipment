using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;

namespace EquipmentRental.Provider
{
    public class InventoryProvider : IInventoryProvider
    {
        readonly IGenericEfRepository<Inventory> _rep;

        public InventoryProvider(IGenericEfRepository<Inventory> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllInventories()
        {
            try
            {
                var item = await _rep.Get();
                var dtOs = Mapper.Map<IEnumerable<InventoryDto>>(item);
                return dtOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }
    }
}
