using Customers.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Validation
{
    public class CreateCustomerCommandRequestDtoValidation : AbstractValidator<CreateCustomerCommandRequestDto>
    {
        public CreateCustomerCommandRequestDtoValidation()
        {
            RuleFor(x => x.City).NotEmpty().NotNull();
            RuleFor(x => x.CityCode).NotNull().NotEmpty();
            RuleFor(x => x.Country).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.OrderId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        }
    }
}
