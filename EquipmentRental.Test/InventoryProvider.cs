using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;
using EquipmentRental.Provider;

namespace EquipmentRental.Test
{
    public class InventoryProvider : IInventoryProvider
    {
        readonly IGenericEfRepository<InventoryDto> _rep;

        public InventoryProvider(IGenericEfRepository<InventoryDto> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllInventories()
        {
            try
            {
                var items = await _rep.Get();
                return items;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }
    }
}
