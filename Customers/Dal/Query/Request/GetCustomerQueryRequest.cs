using Customers.Dal.Core;
using Customers.Dal.Query.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dal.Query.Request
{
    public class GetCustomerQueryRequest : IQueryRequest<GetCustomerQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
