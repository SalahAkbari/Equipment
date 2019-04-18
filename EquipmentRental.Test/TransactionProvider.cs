using System;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider;
using EquipmentRental.Provider.Utilities;
using EquipmentRental.Provider.ViewModels;

namespace EquipmentRental.Test
{
    public class TransactionProvider : ITransactionProvider
    {
        readonly IGenericEfRepository<TransactionDTo> _rep;

        public TransactionProvider(IGenericEfRepository<TransactionDTo> rep)
        {
            _rep = rep;
        }

        public TransactionDTo AddTransaction(string customerId, TransactionDTo transaction)
        {
            try
            {
                transaction.Points = Helper.CalculatePoints(transaction.Type);
                transaction.Price = Helper.CalculatePrice(transaction.Days, transaction.Type);
                transaction.UserId = customerId;
                _rep.Add(transaction);
                if (!_rep.Save()) return null;
                return transaction;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw e;
            }
        }

        public async Task<Invoice> GetAllTransactions(string customerId)
        {
            try
            {
                var items = (await _rep.Get()).Where(b => b.UserId.Equals(customerId));
                Invoice invoice = new Invoice
                {
                    Transactions = items,
                    TotalPoints = items.Sum(c => c.Points),
                    TotalPrice = items.Sum(c => c.Price)
                };
                return invoice;
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
                return item;
            }
            catch (Exception e)
            {
                //Logger.ErrorException(e.Message, e);
                throw;
            }
        }
    }
}
