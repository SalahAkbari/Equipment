using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.Domain.DTOs;

namespace EquipmentRental.Provider
{
    public class InventoryProvider : IInventoryProvider
    {
        //readonly IGenericEfRepository<Customer> _rep;

        //public InventoryProvider(IGenericEfRepository<Customer> rep)
        //{
        //    _rep = rep;
        //}

        public async Task<IEnumerable<InventoryDto>> GetAllInventories()
        {
            throw new NotImplementedException();
            //try
            //{
            //    var item = await _rep.Get();
            //    var dtOs = Mapper.Map<IEnumerable<CustomerDto>>(item);
            //    return dtOs;
            //}
            //catch (Exception e)
            //{
            //    //Logger.ErrorException(e.Message, e);
            //    throw e;
            //}
        }
    }
}
