using Customers.Business.Concrete;
using Customers.Dal.Command.Request;
using Customers.Dal.Command.Response;
using Customers.Dal.Query.Request;
using Customers.Dal.Query.Response;
using Customers.Dto;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TesodevUnitTest
{
    [TestFixture]
    class CustomerTest
    {
        [TestCase("","ss",32,"sr","a@a.com","d0ddc2e7-56c8-4cd2-9f29-811336565cbd","sss","d0ddc2e7-56c8-4cd2-9f29-811336565cbd")]
        public async Task CreateCustomerTest_ReturnsTrue(string addressLine,string city, int cityCode, string country,string email, Guid id, string name, Guid orderId)
        {
            CreateCustomerCommandRequestDto request = new CreateCustomerCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Email = email,
                Id = id,
                Name = name,
                OrderId = orderId
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<CreateCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CreateCustomerCommandResponse { Id = request.Id });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.CreateAsync(request);
            Assert.AreEqual(true, data.Success);
        }
        public const string empty = "{00000000-0000-0000-0000-000000000000}";
        public const string orderId = "{d0ddc2e7-56c8-4cd2-9f21-811336565cbd}";
        public const string Id = "{d0ddc2e7-56c8-4cd2-9f29-811336565cbd}";
        [TestCase("", "ss", 32, "sr", "a@a.com", empty, "sss", orderId)]
        [TestCase("", "ss", 32, "sr", "a@a.com", Id, "sss", empty)]
        [TestCase("", "ss", 32, "sr", "a@a.com", Id, "", orderId)]
        [TestCase("", "ss", 32, "", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "ss", 0, "sr", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "", 32, "sr", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "ss", 32, "sr", "", Id, "sss", orderId)]
        [TestCase("", "s", 0, "", "", empty, "", empty)]
        public async Task CreateCustomerTest_ReturnsFalse(string addressLine, string city, int cityCode, string country, string email, Guid id, string name, Guid orderId)
        {
            CreateCustomerCommandRequestDto request = new CreateCustomerCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Email = email,
                Id = id,
                Name = name,
                OrderId = orderId
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<CreateCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new CreateCustomerCommandResponse { Id = request.Id });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.CreateAsync(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase("", "ss", 32, "sr", "a@a.com", "d0ddc2e7-56c8-4cd2-9f29-811336565cbd", "sss", "d0ddc2e7-56c8-4cd2-9f29-811336565cbd")]
        public async Task UpdateCustomerTest_ReturnsTrue(string addressLine, string city, int cityCode, string country, string email, Guid id, string name, Guid orderId)
        {
            UpdateCustomerCommandRequestDto request = new UpdateCustomerCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Email = email,
                Id = id,
                Name = name,
                OrderId = orderId
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateCustomerCommandResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.UpdateAsync(request);
            Assert.AreEqual(true, data.Success);
        }

        [TestCase("", "ss", 32, "sr", "a@a.com", empty, "sss", orderId)]
        [TestCase("", "ss", 32, "sr", "a@a.com", Id, "sss", empty)]
        [TestCase("", "ss", 32, "sr", "a@a.com", Id, "", orderId)]
        [TestCase("", "ss", 32, "", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "ss", 0, "sr", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "", 32, "sr", "a@a.com", Id, "sss", orderId)]
        [TestCase("", "ss", 32, "sr", "", Id, "sss", orderId)]
        [TestCase("", "s", 0, "", "", empty, "", empty)]
        public async Task UpdateCustomerTest_ReturnsFalse(string addressLine, string city, int cityCode, string country, string email, Guid id, string name, Guid orderId)
        {
            UpdateCustomerCommandRequestDto request = new UpdateCustomerCommandRequestDto
            {
                AddressLine = addressLine,
                City = city,
                CityCode = cityCode,
                Country = country,
                Email = email,
                Id = id,
                Name = name,
                OrderId = orderId                
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<UpdateCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new UpdateCustomerCommandResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.UpdateAsync(request);
            Assert.AreEqual(false, data.Success);
        }
        [TestCase(Id)]
        public async Task DeleteCustomerTest_ReturnsTrue(Guid id)
        {
            DeleteCustomerCommandRequest request = new DeleteCustomerCommandRequest
            {                
                Id = id                
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<DeleteCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteCustomerCommandResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.DeleteAsync(request);
            Assert.AreEqual(true, data.Success);
        }
        [TestCase(empty)]
        public async Task DeleteCustomerTest_Returnsfalse(Guid id)
        {
            DeleteCustomerCommandRequest request = new DeleteCustomerCommandRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<DeleteCustomerCommandRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DeleteCustomerCommandResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.DeleteAsync(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase(Id)]
        public async Task ValidateCustomerTest_ReturnsTrue(Guid id)
        {
            ValidateCustomerQueryRequest request = new ValidateCustomerQueryRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<ValidateCustomerQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidateCustomerQueryResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.ValidateCustomer(request);
            Assert.AreEqual(true, data.Success);
        }
        [TestCase(empty)]
        public async Task ValidateCustomerTest_Returnsfalse(Guid id)
        {
            ValidateCustomerQueryRequest request = new ValidateCustomerQueryRequest
            {
                Id = id
            };
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<ValidateCustomerQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ValidateCustomerQueryResponse { Success = true });
            var customerService = new CustomerService(fakeMediator.Object);
            var data = await customerService.ValidateCustomer(request);
            Assert.AreEqual(false, data.Success);
        }

        [TestCase(Id)]
        public async Task GetCustomerTest_ReturnsTrue(Guid id)
        {
            GetCustomerQueryRequest request = new GetCustomerQueryRequest { Id = id };
            GetCustomerQueryResponse getCustomerQueryResponses = new GetCustomerQueryResponse();
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<GetCustomerQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getCustomerQueryResponses);
            var addressService = new CustomerService(fakeMediator.Object);
            var data = await addressService.GetCustomer(request);
            Assert.AreEqual(true, data.Success);
        }

        [TestCase(empty)]
        public async Task GetCustomerTest_ReturnsFalse(Guid id)
        {
            GetCustomerQueryRequest request = new GetCustomerQueryRequest { Id = id };
            GetCustomerQueryResponse getCustomerQueryResponses = new GetCustomerQueryResponse();
            var fakeMediator = new Mock<IMediator>();
            fakeMediator.Setup(x => x.Send(It.IsAny<GetCustomerQueryRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(getCustomerQueryResponses);
            var addressService = new CustomerService(fakeMediator.Object);
            var data = await addressService.GetCustomer(request);
            Assert.AreEqual(false, data.Success);
        }      
    }
}
