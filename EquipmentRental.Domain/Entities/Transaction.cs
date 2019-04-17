using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentRental.Domain.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int EquipmentId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}")]
        public DateTime TransactionDateTime { get; set; }

        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        public int Points { get; set; }

        public int Days { get; set; }

        [ForeignKey("UserId")]
        public virtual Customer User { get; set; }

        
        [ForeignKey("EquipmentId")]
        public virtual Inventory Inventory { get; set; }
    }
}
