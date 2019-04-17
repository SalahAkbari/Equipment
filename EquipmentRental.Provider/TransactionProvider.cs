using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Domain.Entities;
using EquipmentRental.Provider;
using EquipmentRental.Provider.Utilities;

namespace CustomerInquiry.Provider
{
    public class TransactionProvider : ITransactionProvider
    {
        readonly IGenericEfRepository<Transaction> _rep;

        public TransactionProvider(IGenericEfRepository<Transaction> rep)
        {
            _rep = rep;
        }

        public async Task<IEnumerable<TransactionDTo>> GetAllTransactions(string customerId)
        {
            try
            {
                var item = (await _rep.Get()).Where(b => b.UserId.Equals(customerId));
                var dtOs = Mapper.Map<IEnumerable<TransactionDTo>>(item);
                return dtOs;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }


        public async Task<TransactionDTo> GetTransaction(string customerId, int id)
        {
            try
            {
                var item = await _rep.Get(id);
                if (item == null || !item.UserId.Equals(customerId)) return null;
                var dto = Mapper.Map<TransactionDTo>(item);
                return dto;

            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }

        public TransactionDTo AddTransaction(string customerId, TransactionDTo transaction)
        {
            try
            {
                transaction.Points = Helper.CalculatePoints(transaction.Type);
                transaction.Price = Helper.CalculatePrice(transaction.Days, transaction.Type);
                var itemToCreate = Mapper.Map<Transaction>(transaction);
                itemToCreate.UserId = customerId;
                _rep.Add(itemToCreate);
                if (!_rep.Save()) return null;
                var transactionDTo = Mapper.Map<TransactionDTo>(itemToCreate);
                return transactionDTo;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }
    }
}
