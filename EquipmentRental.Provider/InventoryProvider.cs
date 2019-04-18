using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace EquipmentRental.Provider
{
    public class InventoryProvider : IInventoryProvider
    {
        readonly IGenericEfRepository<Inventory> _rep;
        private readonly ILogger<InventoryProvider> _logger;


        public InventoryProvider(IGenericEfRepository<Inventory> rep, ILogger<InventoryProvider> logger)
        {
            _rep = rep;
            _logger = logger;
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
                _logger.LogInformation(e.Message);
                throw e;
            }
        }
    }
}
