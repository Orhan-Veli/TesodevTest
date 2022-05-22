using Customers.Dal.Command.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public class DeleteCustomerCommandRequestValidation : AbstractValidator<DeleteCustomerCommandRequest>
    {
        public DeleteCustomerCommandRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
