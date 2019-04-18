using EquipmentRental.Domain.DTOs;
using System.Collections.Generic;

namespace EquipmentRental.Provider.ViewModels
{
    public class Invoice
    {
        public IEnumerable<TransactionDTo> Transactions { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalPoints { get; set; }
        public string CustomerName { get; } = "Admin";

    }
}
