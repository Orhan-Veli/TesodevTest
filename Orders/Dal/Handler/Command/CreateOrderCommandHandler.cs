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
    public class CreateOrderCommandHandler : ICommandRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        private readonly IConfiguration _configuration;
        public CreateOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string createOrder = @"insert into [Order] values (@Id,@CustomerId,@Price,@Quantity,@Status,@AddressLine,@City,@Country,@CityCode,@CreatedAt,@UpdatedAt)";
                        string createOrderRelation = @"insert into OrderProductRelation values (@Id,@CustomerId,@OrderId)";
                        Guid orderProductRelationGuid = Guid.NewGuid();
                        var createResult = await connection.ExecuteAsync(createOrder, new
                        {
                            request.Id,
                            request.CustomerId,
                            request.Price,
                            request.Quantity,
                            request.Status,                           
                            request.AddressLine,
                            request.City,
                            request.Country,
                            request.CityCode,
                            request.CreatedAt,
                            request.UpdatedAt
                        },transaction);
                        var createRelationResult = await connection.ExecuteAsync(createOrderRelation, new
                        {
                           Id = orderProductRelationGuid,
                           CustomerId = request.Id,
                           OrderId = request.ProductId
                        },transaction);
                        transaction.Commit();
                        return new CreateOrderCommandResponse { Id = request.Id };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new CreateOrderCommandResponse { Id = Guid.Empty };
                    }
                }
            }
        }
    }
}
