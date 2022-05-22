using Customers.Dal.Command.Response;
using Customers.Dal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dal.Command.Request
{
    public class DeleteCustomerCommandRequest : ICommandRequest<DeleteCustomerCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
