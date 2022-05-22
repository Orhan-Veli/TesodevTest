using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dal.Query.Response
{
    public class GetAllCustomerQueryResponse
    {
        public List<GetCustomerQueryResponse> Customers { get; set; }
    }
}
