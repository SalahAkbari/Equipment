using EquipmentRental.Domain.DTOs;
using System;
using System.Collections.Generic;

namespace EquipmentRental.Test
{
    public class MockData
    {
        public static MockData Current { get; } = new MockData();
        public List<InventoryDto> Inventories { get; set; }

        public MockData()
        {
            Inventories = new List<InventoryDto>()
            {
                new InventoryDto() { InventoryID = 1,
                    Name = "Caterpillar bulldozer", Type = Domain.Enums.EquipmentType.Heavy },
                new InventoryDto() { InventoryID = 2,
                    Name = "KamAZ truck", Type = Domain.Enums.EquipmentType.Regular },
                new InventoryDto() { InventoryID = 3,
                    Name = "Komatsu crane", Type = Domain.Enums.EquipmentType.Heavy },
                new InventoryDto() { InventoryID = 4,
                    Name = "Volvo steamroller", Type = Domain.Enums.EquipmentType.Regular },
                new InventoryDto() { InventoryID = 5,
                    Name = "Bosch jackhammer", Type = Domain.Enums.EquipmentType.Specialized }
            };
        }
    }
}
