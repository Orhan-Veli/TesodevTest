using FluentValidation;
using Orders.Dal.Query.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Validation
{
    public class GetOrderQueryRequestValidation : AbstractValidator<GetOrderQueryRequest>
    {
        public GetOrderQueryRequestValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
