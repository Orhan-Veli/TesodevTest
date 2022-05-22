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
    public class GetCustomerQueryHandler : IQueryRequestHandler<GetCustomerQueryRequest, GetCustomerQueryResponse>
    {
        private readonly IConfiguration _configuration;
        public GetCustomerQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GetCustomerQueryResponse> Handle(GetCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string getSql = @"select * from customer where Id = @Id";
                var result = await connection.QueryAsync<GetCustomerQueryResponse>(getSql, new { request.Id });
                return new GetCustomerQueryResponse 
                { 
                    Id = result.FirstOrDefault().Id, 
                    UpdatedAt = result.FirstOrDefault().UpdatedAt, 
                    AddressLine = result.FirstOrDefault().AddressLine, 
                    City = result.FirstOrDefault().City, 
                    CityCode = result.FirstOrDefault().CityCode, 
                    Country = result.FirstOrDefault().Country, 
                    CreatedAt = result.FirstOrDefault().CreatedAt, 
                    Email = result.FirstOrDefault().Email, 
                    Name = result.FirstOrDefault().Name, 
                    OrderId = result.FirstOrDefault().OrderId                   
                };
            }
        }
    }
}
