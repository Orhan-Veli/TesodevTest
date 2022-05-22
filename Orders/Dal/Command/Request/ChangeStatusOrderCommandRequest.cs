using Orders.Dal.Command.Response;
using Orders.Dal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Command.Request
{
    public class ChangeStatusOrderCommandRequest : ICommandRequest<ChangeStatusOrderCommandResponse>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
