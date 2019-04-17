using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EquipmentRental.Domain.Entities
{ 
    public class Customer : IdentityUser
    {
        #region Properties
        public string DisplayName { get; set; }
        public string Notes { get; set; }
        [Required]
        public int Type { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        #endregion

        #region Lazy-Load Properties
        /// <summary>
        /// A list of all the transaction created by this customer.
        /// </summary>
        public virtual List<Transaction> Transactions { get; set; }
        #endregion
    }
}
