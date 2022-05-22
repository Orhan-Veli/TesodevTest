using Customers.Dal.Core;
using Customers.Dal.Query.Request;
using Customers.Dal.Query.Response;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Customers.Dal.Handler.QueryHandler
{
    public class GetAllCustomerQueryHandler : IQueryRequestHandler<GetAllCustomerQueryRequest, GetAllCustomerQueryResponse>
    {
        private readonly IConfiguration _configuration;
        public GetAllCustomerQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GetAllCustomerQueryResponse> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string getAllCustomer = @"select * from customer";
                var result = await connection.QueryAsync<GetCustomerQueryResponse>(getAllCustomer);
                return new GetAllCustomerQueryResponse { Customers = result.ToList() };
            }
        }
    }
}
