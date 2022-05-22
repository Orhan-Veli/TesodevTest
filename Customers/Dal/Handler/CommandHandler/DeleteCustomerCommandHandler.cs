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
    public class DeleteCustomerCommandHandler : ICommandRequestHandler<DeleteCustomerCommandRequest, DeleteCustomerCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public DeleteCustomerCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<DeleteCustomerCommandResponse> Handle(DeleteCustomerCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteCustomer = @"delete from Customer where Id = @Id";
                        string deleteCustomerRelation = @"delete from CustomerOrderRelation where CustomerId = @Id";
                        var createRelationResult = await connection.ExecuteAsync(deleteCustomerRelation, new
                        {
                            request.Id
                        },transaction);
                        var createResult = await connection.ExecuteAsync(deleteCustomer, new
                        {
                            request.Id                            
                        },transaction);                        
                        transaction.Commit();
                        return new DeleteCustomerCommandResponse { Success = true };
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new DeleteCustomerCommandResponse { Success = false };
                    }
                }
            }
        }
    }
}
