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
    public class UpdateCustomerCommandHandler : ICommandRequestHandler<UpdateCustomerCommandRequest, UpdateCustomerCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public UpdateCustomerCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UpdateCustomerCommandResponse> Handle(UpdateCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string updateCustomer = @"update Customer
                                                    set [Name] = @Name,
                                                    Email = @Email,
                                                    AddressLine = @AddressLine,
                                                    City = @City,
                                                    Country = @Country,
                                                    CityCode = @CityCode,
                                                    UpdatedAt = @UpdatedAt
                                                    where Id = @Id";                        
               
                        var createResult = await connection.ExecuteAsync(updateCustomer, new
                        {                           
                            request.Name,
                            request.Email,
                            request.AddressLine,
                            request.City,
                            request.Country,
                            request.CityCode,
                            request.UpdatedAt,
                            request.Id
                        },transaction);                        
                        transaction.Commit();
                        return new UpdateCustomerCommandResponse { Success = true };
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new UpdateCustomerCommandResponse { Success = false };
                    }
                }
            }
        }
    }
}
