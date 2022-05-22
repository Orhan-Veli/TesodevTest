using Customers.Business.Abstract;
using Customers.Constants;
using Customers.Dal.Command.Request;
using Customers.Dal.Command.Response;
using Customers.Dal.Query.Request;
using Customers.Dal.Query.Response;
using Customers.Dto;
using Customers.Utilities.Abstract;
using Customers.Utilities.Concerete;
using Mapster;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediatr;
        public CustomerService(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        public async Task<IResult<CreateCustomerCommandResponse>> CreateAsync(CreateCustomerCommandRequestDto request)
        {
            if (request == null ||
               string.IsNullOrEmpty(request.City) ||
               request.CityCode == 0 ||
               string.IsNullOrEmpty(request.Country) ||
               string.IsNullOrEmpty(request.Email) ||
               string.IsNullOrEmpty(request.Name) ||
               request.Id == Guid.Empty ||
               request.OrderId == Guid.Empty
               )
            {
                Log.Logger.Information("Customer model not valid" + DateTime.Now);
                return new Result<CreateCustomerCommandResponse>(false, Message.ModelIsNotValid);
            }

            var createCustomer = request.Adapt<CreateCustomerCommandRequest>();
            createCustomer.CreatedAt = DateTime.Now;
            createCustomer.UpdatedAt = DateTime.Now;

            var result = await _mediatr.Send(createCustomer);
            if(result.Id == Guid.Empty)
            {
                Log.Logger.Information("Customer Guid is Empty " + DateTime.Now);
                return new Result<CreateCustomerCommandResponse>(false, Message.CustomerNotCreated);
            }

            Log.Logger.Information("Customer Created! " + DateTime.Now);
            return new Result<CreateCustomerCommandResponse>(true, new CreateCustomerCommandResponse { Id = result.Id });
         }

        public async Task<IResult<DeleteCustomerCommandResponse>> DeleteAsync(DeleteCustomerCommandRequest request)
        {
            if(request.Id == Guid.Empty)
            {
                Log.Logger.Information("Customer Guid is Empty " + DateTime.Now);
                return new Result<DeleteCustomerCommandResponse>(false, Message.ModelIsNotValid);
            }

            var deleteCustomer = await _mediatr.Send(request);

            if (!deleteCustomer.Success)
            {
                Log.Logger.Information("Customer could not be deleted" + DateTime.Now);
                return new Result<DeleteCustomerCommandResponse>(false, Message.CustomerNotDeleted);
            }

            Log.Logger.Information("Customer deleted" + DateTime.Now);
            return new Result<DeleteCustomerCommandResponse>(deleteCustomer.Success);
        }

        public async Task<IResult<GetAllCustomerCommandResponseDto>> GetAllCustomer(GetAllCustomerQueryRequest request)
        {
            var getAllCustomer = await _mediatr.Send(request);
            if(!getAllCustomer.Customers.Any())
            {
                Log.Logger.Information("There are no customers in the database" + DateTime.Now);
                return new Result<GetAllCustomerCommandResponseDto>(false,Message.CustomerCountZero);
            }
            var customerGetAllDto = getAllCustomer.Adapt<GetAllCustomerCommandResponseDto>();

            Log.Logger.Information("Customers listed" + DateTime.Now);
            return new Result<GetAllCustomerCommandResponseDto>(true, customerGetAllDto);
        }

        public async Task<IResult<GetCustomerCommandResponseDto>> GetCustomer(GetCustomerQueryRequest request)
        {
            if(request.Id == Guid.Empty)
            {
                Log.Logger.Information("Id is empty" + DateTime.Now);
                return new Result<GetCustomerCommandResponseDto>(false, Message.ModelIsNotValid);
            }

            var getCustomer = await _mediatr.Send(request);
            if (getCustomer == null)
            {
                Log.Logger.Information("Customer is not found" + DateTime.Now);
                return new Result<GetCustomerCommandResponseDto>(false, Message.CustomerCountZero);
            }
            var customerGetDto = getCustomer.Adapt<GetCustomerCommandResponseDto>();

            Log.Logger.Information("Customers listed" + DateTime.Now);
            return new Result<GetCustomerCommandResponseDto>(true, customerGetDto);
        }

        public async Task<IResult<UpdateCustomerCommandResponse>> UpdateAsync(UpdateCustomerCommandRequestDto request)
        {
            if (request == null ||
               string.IsNullOrEmpty(request.City) ||
               request.CityCode == 0 ||
               string.IsNullOrEmpty(request.Country) ||
               string.IsNullOrEmpty(request.Email) ||
               string.IsNullOrEmpty(request.Name)  ||
               request.OrderId == Guid.Empty ||
               request.Id == Guid.Empty                
               )
            {
                Log.Logger.Information("Customer model not valid" + DateTime.Now);
                return new Result<UpdateCustomerCommandResponse>(false, Message.ModelIsNotValid);
            }
            var updateCustomer = request.Adapt<UpdateCustomerCommandRequest>();
            updateCustomer.UpdatedAt = DateTime.Now;

            var result = await _mediatr.Send(updateCustomer);
            if (!result.Success)
            {
                Log.Logger.Information("Customer could not be updated " + DateTime.Now);
                return new Result<UpdateCustomerCommandResponse>(false, Message.CustomerNotUpdated);
            }

            Log.Logger.Information("Customer Updated! " + DateTime.Now);
            return new Result<UpdateCustomerCommandResponse>(result.Success);
        }

        public async Task<IResult<ValidateCustomerQueryResponse>> ValidateCustomer(ValidateCustomerQueryRequest request)
        {
            if(request.Id == Guid.Empty)
            {
                Log.Logger.Information("Customer Guid is Empty " + DateTime.Now);
                return new Result<ValidateCustomerQueryResponse>(false, Message.CustomerNotValid);
            }

            var validateResult = await _mediatr.Send(request);

            if(!validateResult.Success)
            {
                Log.Logger.Information("Validate result false " + DateTime.Now);
                return new Result<ValidateCustomerQueryResponse>(false, Message.CustomerNotValid);
            }
            return new Result<ValidateCustomerQueryResponse>(validateResult.Success);
        }
    }
}
