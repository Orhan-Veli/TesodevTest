using Dapper;
using Microsoft.Extensions.Configuration;
using Orders.Dal.Core;
using Orders.Dal.Query.Request;
using Orders.Dal.Query.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Dal.Handler.Query
{
    public class GetAllOrderQueryHandler : IQueryRequestHandler<GetAllOrderQueryRequest, GetAllOrderQueryResponse>
    {
        private readonly IConfiguration _configuration;

        public GetAllOrderQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GetAllOrderQueryResponse> Handle(GetAllOrderQueryRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string getAllOrder = @"select * from [Order]";
                var result = await connection.QueryAsync<GetOrderQueryResponse> (getAllOrder);
                return new GetAllOrderQueryResponse { Orders = result.ToList() };
            }
        }
    }
}
