﻿using EquipmentRental.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EquipmentRental.Domain.DTOs
{
    public class TransactionDTo
    {
        public int TransactionID { get; set; }
        public string UserId { get; set; }
        public int EquipmentId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public string TransactionDateTime { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        public int Points { get; set; }

        public int Days { get; set; }
        public EquipmentType Type { get; set; }
    }
}
