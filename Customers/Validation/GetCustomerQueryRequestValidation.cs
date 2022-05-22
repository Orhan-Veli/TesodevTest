using Customers.Dal.Query.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public class GetCustomerQueryRequestValidation : AbstractValidator<GetCustomerQueryRequest>
    {
        public GetCustomerQueryRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
