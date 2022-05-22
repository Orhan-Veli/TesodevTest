using FluentValidation;
using Orders.Dal.Command.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Validation
{
    public class ChangeStatusOrderCommandRequestValidation : AbstractValidator<ChangeStatusOrderCommandRequest>
    {
        public ChangeStatusOrderCommandRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Status).NotNull().NotEmpty();
        }
    }
}
