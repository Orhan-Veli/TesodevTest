using Customers.Dal.Query.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public class ValidateCustomerQueryRequestValidation : AbstractValidator<ValidateCustomerQueryRequest>
    {
        public ValidateCustomerQueryRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
