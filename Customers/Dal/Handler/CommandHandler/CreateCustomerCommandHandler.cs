using Customers.Dal.Command.Request;
using Customers.Dal.Command.Response;
using Customers.Dal.Core;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Dal.Handler.CommandHandler
{
    public class CreateCustomerCommandHandler : ICommandRequestHandler<CreateCustomerCommandRequest, CreateCustomerCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public CreateCustomerCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string createCustomer = @"insert into Customer values (@Id,@Name,@Email,@AddressLine,@City,@Country,@CityCode,@CreatedAt,@UpdatedAt)";
                        string createCustomerRelation = @"insert into CustomerOrderRelation values (@Id,@CustomerId,@OrderId)";
                        Guid customerRelationGuid = Guid.NewGuid();
                        var createResult = await connection.ExecuteAsync(createCustomer, new
                        {
                            request.Id,
                            request.Name,
                            request.Email,
                            request.AddressLine,
                            request.City,
                            request.Country,
                            request.CityCode,
                            request.CreatedAt,
                            request.UpdatedAt,
                        },transaction);
                        var createRelationResult = await connection.ExecuteAsync(createCustomerRelation, new
                        {
                           Id = customerRelationGuid,
                           CustomerId = request.Id,
                           OrderId = request.OrderId
                        }, transaction);
                        transaction.Commit();
                        return new CreateCustomerCommandResponse { Id = request.Id };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new CreateCustomerCommandResponse { Id = Guid.Empty };
                    }
                }                  
            }
        }
    }
}
