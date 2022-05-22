using Orders.Dal.Command.Response;
using Orders.Dal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Command.Request
{
    public class DeleteOrderCommandRequest : ICommandRequest<DeleteOrderCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
