using System;
using System.Collections.Generic;

namespace EquipmentRental.DataAccess
{
    public static class Fee
    {
        public static Dictionary<string, decimal> Fees = new Dictionary<string, decimal>
        {
                {"One-Time",  100M},
                {"Premium",  60M},
                {"Regular",  40M},
       };
    }
}
