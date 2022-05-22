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
    public class ValidateCustomerQueryHandler : IQueryRequestHandler<ValidateCustomerQueryRequest, ValidateCustomerQueryResponse>
    {
        private readonly IConfiguration _configuration;
        public ValidateCustomerQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ValidateCustomerQueryResponse> Handle(ValidateCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                string validateSql = @"select * from customer where Id = @Id";
                var result = await connection.QueryAsync(validateSql, new { request.Id });
                if (result.Any())
                {
                    return new ValidateCustomerQueryResponse { Success = true };
                }
                return new ValidateCustomerQueryResponse { Success = false };
            }
        }
    }
}
