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
    public class ChangeStatusOrderCommandHandler : ICommandRequestHandler<ChangeStatusOrderCommandRequest, ChangeStatusOrderCommandResponse>
    {
        private readonly IConfiguration _configuration;

        public ChangeStatusOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ChangeStatusOrderCommandResponse> Handle(ChangeStatusOrderCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string createCustomer = @"update [Order] set Status = @Status where Id = @Id";                      
                        var createResult = await connection.ExecuteAsync(createCustomer, new
                        {
                            request.Id,
                            request.Status
                        },transaction);                        
                        transaction.Commit();
                        return new ChangeStatusOrderCommandResponse { Success = true };
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new ChangeStatusOrderCommandResponse { Success = false };
                    }
                }
            }
        }
    }
}
