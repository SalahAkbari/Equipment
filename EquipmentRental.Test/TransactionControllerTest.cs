using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using EquipmentRental.Controllers;
using EquipmentRental.DataAccess;
using EquipmentRental.Domain.DTOs;
using EquipmentRental.Provider;
using EquipmentRental.Provider.ViewModels;
using EquipmentRental.Domain.Enums;

namespace EquipmentRental.Test
{
    public class TransactionControllerTest
    {
        readonly TransactionController _controller;
        private readonly Mock<IGenericEfRepository<TransactionDTo>> _mockRepo;


        public TransactionControllerTest()
        {
            _mockRepo = new Mock<IGenericEfRepository<TransactionDTo>>();
            ITransactionProvider provider = new TransactionProvider(_mockRepo.Object);

            _controller = new TransactionController(provider);

            _mockRepo.Setup(m => m.Get())
                .Returns(Task.FromResult(MockData.Current.Transactions.AsEnumerable()));
        }

        private void MoqSetup(int transactionId)
        {
            _mockRepo.Setup(x => x.Get(It.Is<int>(y => y == transactionId)))
                .Returns(Task.FromResult(MockData.Current.Transactions
                    .FirstOrDefault(p => p.TransactionID.Equals(transactionId))));
        }

        private void MoqSetupAdd(TransactionDTo testItem)
        {
            _mockRepo.Setup(x => x.Add(It.Is<TransactionDTo>(y => y == testItem)))
                .Callback<TransactionDTo>(s => MockData.Current.Transactions.Add(s));
            _mockRepo.Setup(x => x.Save()).Returns(true);
        }


        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";
            // Act
            var okResult = await _controller.Get(testCustomerId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";

            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            // Assert
            var items = Assert.IsType<Invoice>(okResult?.Value);
            Assert.Equal(5, items.Transactions.Count());
        }

        [Fact]
        public async Task GetById_UnknownCustomerIdAndTransactionIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.Get("4", 88);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Theory]//Based on MockData.Current.Transactions
        [InlineData(1, 2)]//Because EquipmentType = Heavy => Points should be 2
        [InlineData(2, 1)]//Because EquipmentType = Regular => Points should be 1
        [InlineData(3, 2)]//Because EquipmentType = Heavy => Points should be 2
        [InlineData(4, 1)]//Because EquipmentType = Regular => Points should be 1
        [InlineData(5, 1)]//Because EquipmentType = Specialized => Points should be 1
        public async Task GetById_EquipmentTypeAndDaysPassed_ReturnsRightPoints(int testTransactionId, int expectedPoints)
        {
            // Arrange
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";
            MoqSetup(testTransactionId);

            // Act
            var okResult = await _controller.Get(testCustomerId, testTransactionId) as OkObjectResult;

            // Assert
            Assert.IsType<TransactionDTo>(okResult?.Value);
            Assert.Equal(expectedPoints, ((TransactionDTo)okResult.Value).Points);
        }

        [Theory]//Based on MockData.Current.Transactions
        [InlineData(1, 160.00)]//Because Day = 1, EquipmentType = Heavy => Price should be 160.00
        [InlineData(2, 160.00)]//Because Day = 2, EquipmentType = Regular => Price should be 160.00
        [InlineData(3, 480.00)]//Because Day = 3, EquipmentType = Heavy => Price should be 480.00
        [InlineData(4, 240.00)]//Because Day = 4, EquipmentType = Regular => Price should be 240.00
        [InlineData(5, 140.00)]//Because Day = 5, EquipmentType = Specialized => Price should be 140.00
        public async Task GetById_EquipmentTypeAndDaysPassed_ReturnsRightPrice(int testTransactionId, decimal expectedPrice)
        {
            // Arrange
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";
            MoqSetup(testTransactionId);

            // Act
            var okResult = await _controller.Get(testCustomerId, testTransactionId) as OkObjectResult;

            // Assert
            Assert.IsType<TransactionDTo>(okResult?.Value);
            Assert.Equal(expectedPrice, ((TransactionDTo)okResult.Value).Price);
        }

        [Fact]//Based on MockData.Current.Transactions
        public async Task GetInvoice_WhenCalled_ReturnedRightTotalPriceAndPoints()
        {
            // Arrange
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";
            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            var item = okResult?.Value as Invoice;

            // Assert
            Assert.Equal(7, item.TotalPoints);
            Assert.Equal(1180, item.TotalPrice);
        }

        [Fact]
        public void Add_NullObjectPassed_ReturnsBadRequest()
        {
            //Arrange
            //ids should be passed into the action with the URL as opposed to 
            //sending them with the request body to follow the REST standard.
            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";

            // Act
            var badResponse = _controller.Post(testCustomerId, null);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Add_MissingObjectDaysPassed_ReturnsBadRequest()
        {
            // Arrange

            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";

            var testItem = new TransactionDTo()
            {
                TransactionID = 6,
                EquipmentId = 1,
                Type = EquipmentType.Heavy.ToString()
            };

            MoqSetupAdd(testItem);

            // Act

            //See how the ValidateViewModel extension method in the Helper class is useful here
            _controller.ValidateViewModel(testItem);
            //I have used the above useful extension method to simulate validation instead of adding customly like below
            //_controller.ModelState.AddModelError("CustomerName", "Required");

            var badResponse = _controller.Post(testCustomerId, testItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedAtRouteResult()
        {
            // Arrange

            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";

            var testItem = new TransactionDTo()
            {
                TransactionID = 6,
                Days = 1,
                EquipmentId = 1,
                Type = EquipmentType.Heavy.ToString()
            };

            MoqSetupAdd(testItem);

            // Act
            _controller.ValidateViewModel(testItem);
            var createdResponse = _controller.Post(testCustomerId, testItem);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedRightPriceAndPoints()
        {
            // Arrange

            const string testCustomerId = "042f780d-8b17-42f3-8c73-486d63f87e98";

            var testItem = new TransactionDTo()
            {
                TransactionID = 6,
                Days = 1,
                EquipmentId = 1,
                Type = EquipmentType.Heavy.ToString()
            };

            MoqSetupAdd(testItem);

            // Act
            _controller.ValidateViewModel(testItem);

            var createdResponse = _controller.Post(testCustomerId, testItem) as CreatedAtRouteResult;
            var item = createdResponse?.Value as TransactionDTo;

            // Assert
            Assert.IsType<TransactionDTo>(item);
            Assert.Equal(2, item.Points);
            Assert.Equal(160, item.Price);
        }
    }
}
