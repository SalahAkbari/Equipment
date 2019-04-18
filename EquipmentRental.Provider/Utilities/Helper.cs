using EquipmentRental.DataAccess;
using EquipmentRental.Domain.Enums;
using EquipmentRental.Provider.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

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

        public static void SaveToTxt(this Invoice invoice)
        {
            using (TextWriter tw = new StreamWriter($"C:\\Invoices\\invoice_ {DateTime.Now.ToString("yyyyMMddHHmmssfff")}.txt"))
            {
                tw.WriteLine($"Customer Name: {invoice.CustomerName} - Total Points: {invoice.TotalPoints}" +
                    $" - Total Price: {invoice.TotalPrice}" 
                    + Environment.NewLine + "---------------------------------------------------------------------------");

                foreach (var item in invoice.Transactions)
                {
                    tw.WriteLine($"Equipment Name: {item.EquipmentName} - Type: {item.Type} - Days: {item.Days} - Price: {item.Price} - TransactionDateTime: {item.TransactionDateTime}");
                }
            }
        }
    }
}
