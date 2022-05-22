using Dapper;
using Microsoft.Extensions.Configuration;
using Orders.Dal.Command.Request;
using Orders.Dal.Command.Response;
using Orders.Dal.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Dal.Handler.Command
{
    public class UpdateOrderCommandHandler : ICommandRequestHandler<UpdateOrderCommandRequest, UpdateOrderCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public UpdateOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string updateCustomer = @"update [Order]                                                    
                                                    set CustomerId = @CustomerId,
                                                    Quantity = @Quantity,
                                                    Price = @Price,
                                                    Status = @Status,
                                                    AddressLine = @AddressLine,
                                                    City = @City,
                                                    Country = @Country,
                                                    CityCode = @CityCode,
                                                    UpdatedAt = @UpdatedAt
                                                    where Id = @Id";

                        var createResult = await connection.ExecuteAsync(updateCustomer, new
                        {
                            request.CustomerId,
                            request.Quantity,
                            request.Price,
                            request.Status,
                            request.AddressLine,
                            request.City,
                            request.Country,
                            request.CityCode,
                            request.UpdatedAt,
                            request.Id
                        },transaction);
                        transaction.Commit();
                        return new UpdateOrderCommandResponse { Success = true };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new UpdateOrderCommandResponse { Success = false };
                    }
                }
            }
        }
    }
}
