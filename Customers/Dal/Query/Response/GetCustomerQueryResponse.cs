
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dal.Query.Response
{
    public class GetCustomerQueryResponse
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int CityCode { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
