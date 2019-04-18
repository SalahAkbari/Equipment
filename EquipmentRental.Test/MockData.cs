using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider.Utilities;
using System;
using System.Collections.Generic;
using EquipmentRental.Domain.Enums;

namespace EquipmentRental.Test
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<InventoryDto> Inventories { get; set; }
        public List<TransactionDTo> Transactions { get; set; }

        public MockData()
        {
            Inventories = new List<InventoryDto>()
            {
                new InventoryDto() { InventoryID = 1,
                    Name = "Caterpillar bulldozer", Type = EquipmentType.Heavy.ToString() },
                new InventoryDto() { InventoryID = 2,
                    Name = "KamAZ truck", Type = EquipmentType.Regular.ToString() },
                new InventoryDto() { InventoryID = 3,
                    Name = "Komatsu crane", Type = EquipmentType.Heavy.ToString() },
                new InventoryDto() { InventoryID = 4,
                    Name = "Volvo steamroller", Type = EquipmentType.Regular.ToString() },
                new InventoryDto() { InventoryID = 5,
                    Name = "Bosch jackhammer", Type = EquipmentType.Specialized.ToString() }
            };

            Transactions = new List<TransactionDTo>()
            {
                new TransactionDTo() { TransactionID = 1, TransactionDateTime = DateTime.Now.ToString(),
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98", Price = Helper.CalculatePrice(1, EquipmentType.Heavy), Days = 1, EquipmentId = 1, Points = Helper.CalculatePoints(EquipmentType.Heavy), Type = EquipmentType.Heavy.ToString() },
                new TransactionDTo() { TransactionID = 2, TransactionDateTime = DateTime.Now.ToString(),
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98", Price = Helper.CalculatePrice(2, EquipmentType.Regular), Days = 2, EquipmentId = 2, Points = Helper.CalculatePoints(EquipmentType.Regular), Type = EquipmentType.Regular.ToString() },
                new TransactionDTo() { TransactionID = 3, TransactionDateTime = DateTime.Now.ToString(),
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98", Price =  Helper.CalculatePrice(3, EquipmentType.Heavy), Days = 3, EquipmentId = 3, Points = Helper.CalculatePoints(EquipmentType.Heavy), Type = EquipmentType.Heavy.ToString() },
                new TransactionDTo() { TransactionID = 4, TransactionDateTime = DateTime.Now.ToString(),
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98", Price =  Helper.CalculatePrice(4, EquipmentType.Regular), Days = 4, EquipmentId = 4, Points = Helper.CalculatePoints(EquipmentType.Regular), Type = EquipmentType.Regular.ToString() },
                new TransactionDTo() { TransactionID = 5, TransactionDateTime = DateTime.Now.ToString(),
                    UserId = "042f780d-8b17-42f3-8c73-486d63f87e98", Price =  Helper.CalculatePrice(5, EquipmentType.Specialized), Days = 5, EquipmentId = 5, Points = Helper.CalculatePoints(EquipmentType.Specialized), Type = EquipmentType.Specialized.ToString() },
            };
        }
    }
}
