using MediatR;
using Moq;
using NUnit.Framework;
using Orders.Business.Concrete;
using Orders.Dal.Command.Request;
using Orders.Dal.Command.Response;
using Orders.Dal.Query.Request;
using Orders.Dal.Query.Response;
using Orders.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesodevUnitTest
{
    [TestFixture]
    class OrderTest
    {
        public const string empty = "{00000000-0000-0000-0000-000000000000}";
        public const string customerId = "{d0ddc2e7-56c8-4cd2-9f21-811336565cbd}";
        public const string productId = "{d0ddc2e7-56c8-4cd2-9f21-811436565cbd}";
        public const string Id = "{d0ddc2e7-56c8-4cd2-9f29-811336565cbd}";
        [TestCase("", "ss", 32, "sr", Id, customerId, 4, productId,3,"aa")]
        public async Task CreateOrderTest_ReturnsTrue(string addressLine, string city, int cityCode, string country, Guid id, Guid customerId,float price , Guid productId, int quantity ,string status)
        {
            CreateOrderCommandRequestDto request = new CreateOrderCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Id = id,
                CustomerId = customerId,
                Price = price,
                ProductId = productId,
                Quantity = quantity,
                Status = status

            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CreateOrderCommandResponse { Id = request.Id });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.CreateAsync(request);
            Assert.AreEqual(true, data.Success);
        }
        [TestCase("", "", 32, "sr", Id, customerId, 2, productId, 3, "aa")]
        [TestCase("", "ss", 0, "sr", Id, customerId, 1, productId, 2, "aa")]
        [TestCase("", "ss", 32, "", Id, customerId, 4, productId, 3, "aa")]
        [TestCase("", "ss", 32, "sr", empty, customerId, 0, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, empty, 5, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 3, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 0, empty, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 0, empty, 0, "")]
        [TestCase("", "", 0, "", empty, empty, 0, empty, 0, "")]
        public async Task CreateOrderTest_ReturnsFalse(string addressLine, string city, int cityCode, string country, Guid id, Guid customerId, float price, Guid productId, int quantity, string status)
        {
            CreateOrderCommandRequestDto request = new CreateOrderCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Id = id,
                CustomerId = customerId,
                Price = price,
                ProductId = productId,
                Quantity = quantity,
                Status = status
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<CreateOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CreateOrderCommandResponse { Id = request.Id });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.CreateAsync(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase("", "ss", 32, "sr", Id, customerId, 4, productId, 3, "aa")]
        public async Task UpdateOrderTest_ReturnsTrue(string addressLine, string city, int cityCode, string country, Guid id, Guid customerId, float price, Guid productId, int quantity, string status)
        {
            UpdateOrderCommandRequestDto request = new UpdateOrderCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Id = id,
                CustomerId = customerId,
                Price = price,
                ProductId = productId,
                Quantity = quantity,
                Status = status
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.UpdateAsync(request);
            Assert.AreEqual(true, data.Success);
        }

        [TestCase("", "", 32, "sr", Id, customerId, 2, productId, 3, "aa")]
        [TestCase("", "ss", 0, "sr", Id, customerId, 1, productId, 2, "aa")]
        [TestCase("", "ss", 32, "", Id, customerId, 4, productId, 3, "aa")]
        [TestCase("", "ss", 32, "sr", empty, customerId, 0, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, empty, 5, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 3, productId, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 0, empty, 0, "aa")]
        [TestCase("", "ss", 32, "sr", Id, customerId, 0, empty, 0, "")]
        [TestCase("", "", 0, "", empty, empty, 0, empty, 0, "")]
        public async Task UpdateOrderTest_ReturnsFalse(string addressLine, string city, int cityCode, string country, Guid id, Guid customerId, float price, Guid productId, int quantity, string status)
        {
            UpdateOrderCommandRequestDto request = new UpdateOrderCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Id = id,
                CustomerId = customerId,
                Price = price,
                ProductId = productId,
                Quantity = quantity,
                Status = status
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<UpdateOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.UpdateAsync(request);
            Assert.AreEqual(false, data.Success);
        }
        [TestCase(Id)]
        public async Task DeleteOrderTest_ReturnsTrue(Guid id)
        {
            DeleteOrderCommandRequest request = new DeleteOrderCommandRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<DeleteOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.DeleteAsync(request);
            Assert.AreEqual(true, data.Success);
        }
        [TestCase(empty)]
        public async Task DeleteOrderTest_Returnsfalse(Guid id)
        {
            DeleteOrderCommandRequest request = new DeleteOrderCommandRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<DeleteOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.DeleteAsync(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase(Id)]
        public async Task ChangeStatusOrderTest_ReturnsTrue(Guid id)
        {
            ChangeStatusOrderCommandRequest request = new ChangeStatusOrderCommandRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<ChangeStatusOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ChangeStatusOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.ChangeStatusOrder(request);
            Assert.AreEqual(true, data.Success);
        }
        [TestCase(empty)]
        public async Task ChangeStatusOrderTest_Returnsfalse(Guid id)
        {
            ChangeStatusOrderCommandRequest request = new ChangeStatusOrderCommandRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<ChangeStatusOrderCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ChangeStatusOrderCommandResponse { Success = true });
            var orderService = new OrderService(fakeMediator.Object);
            var data = await orderService.ChangeStatusOrder(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase(Id)]
        public async Task GetOrderTest_ReturnsTrue(Guid id)
        {
            GetOrderQueryRequest request = new GetOrderQueryRequest { Id = id };
            GetOrderQueryResponse getOrderQueryResponses = new GetOrderQueryResponse();
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<GetOrderQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getOrderQueryResponses);
            var addressService = new OrderService(fakeMediator.Object);
            var data = await addressService.GetOrder(request);
            Assert.AreEqual(true, data.Success);
        }

        [TestCase(empty)]
        public async Task GetOrderTest_ReturnsFalse(Guid id)
        {
            GetOrderQueryRequest request = new GetOrderQueryRequest { Id = id };
            GetOrderQueryResponse getOrderQueryResponses = new GetOrderQueryResponse();
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<GetOrderQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getOrderQueryResponses);
            var addressService = new OrderService(fakeMediator.Object);
            var data = await addressService.GetOrder(request);
            Assert.AreEqual(false, data.Success);
        }
    }
}

