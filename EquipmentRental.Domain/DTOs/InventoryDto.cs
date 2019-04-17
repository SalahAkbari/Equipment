using EquipmentRental.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.Domain.DTOs
{
    public class InventoryDto
    {
        public int InventoryID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
    }
}
