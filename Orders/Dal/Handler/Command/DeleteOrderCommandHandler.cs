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
    public class DeleteOrderCommandHandler : ICommandRequestHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public DeleteOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string deleteOrder = @"delete from [Order] where Id = @Id";
                        string deleteOrderRelation = @"delete from OrderProductRelation where OrderId = @Id";
                        var createRelationResult = await connection.ExecuteAsync(deleteOrderRelation, new
                        {
                            request.Id
                        },transaction);
                        var createResult = await connection.ExecuteAsync(deleteOrder, new
                        {
                            request.Id
                        },transaction);
                        transaction.Commit();
                        return new DeleteOrderCommandResponse { Success = true };
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return new DeleteOrderCommandResponse { Success = false };
                    }
                }
            }
        }
    }
}
