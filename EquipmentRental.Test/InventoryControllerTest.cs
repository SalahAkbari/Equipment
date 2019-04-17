using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EquipmentRental.Controllers;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider;

namespace EquipmentRental.Test
{
    public class InventoryControllerTest
    {
        readonly InventoryController _controller;
        private readonly Mock<IGenericEfRepository<InventoryDto>> _mockRepo;


        public InventoryControllerTest()
        {
            _mockRepo = new Mock<IGenericEfRepository<InventoryDto>>();
            IInventoryProvider provider = new InventoryProvider(_mockRepo.Object);
            _controller = new InventoryController(provider);

            _mockRepo.Setup(m => m.Get())
                .Returns(Task.FromResult(MockData.Current.Inventories.AsEnumerable()));
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.GetInventories();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.GetInventories() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<InventoryDto>>(okResult?.Value);
            Assert.Equal(5, items.Count);
        }
    }
}
