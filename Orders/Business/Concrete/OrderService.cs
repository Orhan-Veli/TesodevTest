using Mapster;
using MediatR;
using Orders.Business.Abstract;
using Orders.Constants;
using Orders.Dal.Command.Request;
using Orders.Dal.Command.Response;
using Orders.Dal.Query.Request;
using Orders.Dto;
using Orders.Utilities.Abstract;
using Orders.Utilities.Concrete;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Orders.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IMediator _mediatr;
        public OrderService(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        public async Task<IResult<CreateOrderCommandResponse>> CreateAsync(CreateOrderCommandRequestDto request)
        {
            if (
               request == null ||
               string.IsNullOrEmpty(request.City) ||
               request.CityCode == 0 ||
               string.IsNullOrEmpty(request.Country) ||
               string.IsNullOrEmpty(request.Status) ||
               request.CustomerId == Guid.Empty ||
               request.Id == Guid.Empty ||
               request.Price == 0.0 ||
               request.Quantity == 0 ||
               request.ProductId == Guid.Empty
               )
            {
                Log.Logger.Information("Order model not valid" + DateTime.Now);
                return new Result<CreateOrderCommandResponse>(false, Message.ModelIsNotValid);
            }

            var createCustomer = request.Adapt<CreateOrderCommandRequest>();
            createCustomer.CreatedAt = DateTime.Now;
            createCustomer.UpdatedAt = DateTime.Now;

            var result = await _mediatr.Send(createCustomer);
            if (result.Id == Guid.Empty)
            {
                Log.Logger.Information("Order Guid is Empty " + DateTime.Now);
                return new Result<CreateOrderCommandResponse>(false, Message.OrderNotCreated);
            }

            Log.Logger.Information("Order Created! " + DateTime.Now);
            return new Result<CreateOrderCommandResponse>(true, new CreateOrderCommandResponse { Id = result.Id });
        }

        public async Task<IResult<DeleteOrderCommandResponse>> DeleteAsync(DeleteOrderCommandRequest request)
        {
            if (request.Id == Guid.Empty)
            {
                Log.Logger.Information("Customer Guid is Empty " + DateTime.Now);
                return new Result<DeleteOrderCommandResponse>(false, Message.ModelIsNotValid);
            }

            var deleteCustomer = await _mediatr.Send(request);

            if (!deleteCustomer.Success)
            {
                Log.Logger.Information("Order could not be deleted" + DateTime.Now);
                return new Result<DeleteOrderCommandResponse>(false, Message.OrderNotDeleted);
            }

            Log.Logger.Information("Order deleted" + DateTime.Now);
            return new Result<DeleteOrderCommandResponse>(deleteCustomer.Success);
        }

        public async Task<IResult<GetAllOrderCommandResponseDto>> GetAllOrder(GetAllOrderQueryRequest request)
        {
            var getAllOrder = await _mediatr.Send(request);
            if (!getAllOrder.Orders.Any())
            {
                Log.Logger.Information("There are no customers in the database" + DateTime.Now);
                return new Result<GetAllOrderCommandResponseDto>(false, Message.OrderCountZero);
            }
            var orderGetAllDto = getAllOrder.Adapt<GetAllOrderCommandResponseDto>();

            Log.Logger.Information("Customers listed" + DateTime.Now);
            return new Result<GetAllOrderCommandResponseDto>(true, orderGetAllDto);
        }

        public async Task<IResult<GetOrderCommandResponseDto>> GetOrder(GetOrderQueryRequest request)
        {
            if(request.Id == Guid.Empty)
            {
                Log.Logger.Information("Id is empty" + DateTime.Now);
                return new Result<GetOrderCommandResponseDto>(false, Message.ModelIsNotValid);
            }

            var getOrder = await _mediatr.Send(request);
            if (getOrder == null)
            {
                Log.Logger.Information("Order is not found" + DateTime.Now);
                return new Result<GetOrderCommandResponseDto>(false, Message.OrderCountZero);
            }
            var customerGetDto = getOrder.Adapt<GetOrderCommandResponseDto>();

            Log.Logger.Information("Orders listed" + DateTime.Now);
            return new Result<GetOrderCommandResponseDto>(true, customerGetDto);
        }

        public async Task<IResult<UpdateOrderCommandResponse>> UpdateAsync(UpdateOrderCommandRequestDto request)
        {
            if (
                request == null ||
               string.IsNullOrEmpty(request.City) ||
               request.CityCode == 0 ||
               string.IsNullOrEmpty(request.Country) ||
               string.IsNullOrEmpty(request.Status) ||
               request.CustomerId == Guid.Empty ||
               request.Id == Guid.Empty ||
               request.Price == 0.0 ||
               request.Quantity == 0 ||
               request.ProductId == Guid.Empty
               )
            {
                Log.Logger.Information("Order model not valid" + DateTime.Now);
                return new Result<UpdateOrderCommandResponse>(false, Message.ModelIsNotValid);
            }

            var updateCustomer = request.Adapt<UpdateOrderCommandRequest>();
            updateCustomer.UpdatedAt = DateTime.Now;
            var result = await _mediatr.Send(updateCustomer);
            if (!result.Success)
            {
                Log.Logger.Information("Order could not be updated " + DateTime.Now);
                return new Result<UpdateOrderCommandResponse>(false, Message.OrderNotUpdated);
            }

            Log.Logger.Information("Order Updated! " + DateTime.Now);
            return new Result<UpdateOrderCommandResponse>(result.Success);
        }

        public async Task<IResult<ChangeStatusOrderCommandResponse>> ChangeStatusOrder(ChangeStatusOrderCommandRequest request)
        {
            if (request.Id == Guid.Empty)
            {
                Log.Logger.Information("Order Guid is Empty " + DateTime.Now);
                return new Result<ChangeStatusOrderCommandResponse>(false, Message.OrderNotValid);
            }

            var changeStatus = await _mediatr.Send(request);

            if (!changeStatus.Success)
            {
                Log.Logger.Information("ChangeStatus result false " + DateTime.Now);
                return new Result<ChangeStatusOrderCommandResponse>(false, Message.OrderNotValid);
            }
            return new Result<ChangeStatusOrderCommandResponse>(changeStatus.Success);
        }
    }
}

