using Customers.Dal.Command.Request;
using Customers.Dal.Command.Response;
using Customers.Dal.Query.Request;
using Customers.Dal.Query.Response;
using Customers.Dto;
using Customers.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IResult<CreateCustomerCommandResponse>> CreateAsync(CreateCustomerCommandRequestDto request);

        Task<IResult<UpdateCustomerCommandResponse>> UpdateAsync(UpdateCustomerCommandRequestDto request);

        Task<IResult<DeleteCustomerCommandResponse>> DeleteAsync(DeleteCustomerCommandRequest request);

        Task<IResult<GetAllCustomerCommandResponseDto>> GetAllCustomer(GetAllCustomerQueryRequest request);

        Task<IResult<GetCustomerCommandResponseDto>> GetCustomer(GetCustomerQueryRequest request);

        Task<IResult<ValidateCustomerQueryResponse>> ValidateCustomer(ValidateCustomerQueryRequest request);

    }
}
