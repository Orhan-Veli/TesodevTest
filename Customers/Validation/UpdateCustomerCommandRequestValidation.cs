using Customers.Dal.Command.Request;
using Customers.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public class UpdateCustomerCommandRequestValidation : AbstractValidator<UpdateCustomerCommandRequestDto>
    {
        public UpdateCustomerCommandRequestValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Country).NotNull().NotEmpty();
            RuleFor(x => x.CityCode).NotNull().NotEmpty().NotEqual(0);
            RuleFor(x => x.City).NotEmpty().NotNull();
            RuleFor(x => x.OrderId).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}
