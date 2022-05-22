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
    public class GetOrderQueryHandler : IQueryRequestHandler<GetOrderQueryRequest, GetOrderQueryResponse>
    {
        private readonly IConfiguration _configuration;
        public GetOrderQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<GetOrderQueryResponse> Handle(GetOrderQueryRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string getCustomer = @"select * from [Order] where Id = @Id";
                var result = await connection.QueryAsync<GetOrderQueryResponse>(getCustomer, new { request.Id });
                return new GetOrderQueryResponse 
                {
                    AddressLine = result.FirstOrDefault().AddressLine,
                    City = result.FirstOrDefault().City,
                    CityCode = result.FirstOrDefault().CityCode,
                    Country = result.FirstOrDefault().Country,
                    CreatedAt = result.FirstOrDefault().CreatedAt,
                    CustomerId = result.FirstOrDefault().CustomerId,
                    Id = result.FirstOrDefault().Id,
                    Price = result.FirstOrDefault().Price,
                    Quantity = result.FirstOrDefault().Quantity,
                    Status = result.FirstOrDefault().Status,
                    UpdatedAt = result.FirstOrDefault().UpdatedAt
                };
            }
        }
    }
}
