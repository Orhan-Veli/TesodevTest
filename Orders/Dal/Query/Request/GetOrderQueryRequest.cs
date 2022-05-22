using Orders.Dal.Core;
using Orders.Dal.Query.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Query.Request
{
    public class GetOrderQueryRequest : IQueryRequest<GetOrderQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
