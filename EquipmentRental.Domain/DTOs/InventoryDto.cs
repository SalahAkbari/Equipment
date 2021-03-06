﻿using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.Domain.DTOs
{
    public class InventoryDto
    {
        public int InventoryID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
