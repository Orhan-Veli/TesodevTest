using FluentValidation;
using Orders.Dal.Command.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Validation
{
    public class DeleteOrderCommandRequestValidation : AbstractValidator<DeleteOrderCommandRequest>
    {
        public DeleteOrderCommandRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
