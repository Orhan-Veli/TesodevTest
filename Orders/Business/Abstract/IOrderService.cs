using Orders.Dal.Command.Request;
using Orders.Dal.Command.Response;
using Orders.Dal.Query.Request;
using Orders.Dto;
using Orders.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Business.Abstract
{
    public interface IOrderService
    {
        Task<IResult<CreateOrderCommandResponse>> CreateAsync(CreateOrderCommandRequestDto request);

        Task<IResult<UpdateOrderCommandResponse>> UpdateAsync(UpdateOrderCommandRequestDto request);

        Task<IResult<DeleteOrderCommandResponse>> DeleteAsync(DeleteOrderCommandRequest request);

        Task<IResult<GetAllOrderCommandResponseDto>> GetAllOrder(GetAllOrderQueryRequest request);

        Task<IResult<GetOrderCommandResponseDto>> GetOrder(GetOrderQueryRequest request);

        Task<IResult<ChangeStatusOrderCommandResponse>> ChangeStatusOrder(ChangeStatusOrderCommandRequest request);
    }
}
