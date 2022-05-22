
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Query.Response
{
    public class GetAllOrderQueryResponse
    {
        public List<GetOrderQueryResponse> Orders { get; set; }
    }
}
