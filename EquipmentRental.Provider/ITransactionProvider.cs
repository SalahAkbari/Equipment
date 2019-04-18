using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider.ViewModels;
using System.Threading.Tasks;

namespace EquipmentRental.Provider
{
    public interface ITransactionProvider
    {
        Task<Invoice> GetAllTransactions(string customerId, IInventoryProvider inventoryProvider);
        Task<TransactionDTo> GetTransaction(string customerId, int id);
        TransactionDTo AddTransaction(string customerId, TransactionDTo transaction);
    }
}
