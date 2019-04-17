using EquipmentRental.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EquipmentRental.Provider
{
    public interface ITransactionProvider
    {
        Task<IEnumerable<TransactionDTo>> GetAllTransactions(string customerId);
        Task<TransactionDTo> GetTransaction(string customerId, int id);
        TransactionDTo AddTransaction(string customerId, TransactionDTo transaction);
    }
}
