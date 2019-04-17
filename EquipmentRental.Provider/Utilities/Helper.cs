using EquipmentRental.DataAccess;
using EquipmentRental.Domain.Enums;
using System.Collections.Generic;

namespace EquipmentRental.Provider.Utilities
{
    public static class Helper
    {
        public static int CalculatePoints(EquipmentType equipmentType)
        {
            return (int)equipmentType == 1 ? 2 : 1;
        }

        public static decimal CalculatePrice(int days, EquipmentType equipmentType)
        {
            Dictionary<int, decimal> dictionary = new Dictionary<int, decimal>()
            {
                {1, days * (Fee.Fees["One-Time"] + Fee.Fees["Premium"])},
                {2, days > 2 ?  ((days - 2) * Fee.Fees["Regular"]) + (Fee.Fees["One-Time"] + Fee.Fees["Premium"])
                                         : ( Fee.Fees["One-Time"] + Fee.Fees["Premium"])},
                {3,  days > 3 ?  ((days - 3) * Fee.Fees["Regular"]) + Fee.Fees["Premium"]
                                         : Fee.Fees["Premium"]},

            };

            decimal value;
            dictionary.TryGetValue((int)equipmentType, out value);

            return value;
        }
    }
}
