using Orders.Dal.Command.Response;
using Orders.Dal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Command.Request
{
    public class UpdateOrderCommandRequest : ICommandRequest<UpdateOrderCommandResponse>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Status { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int CityCode { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
