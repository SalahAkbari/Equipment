using EquipmentRental.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentRental.Domain.Entities
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public EquipmentType Type { get; set; }

        public virtual List<Transaction> Transactions { get; set; }
    }
}
