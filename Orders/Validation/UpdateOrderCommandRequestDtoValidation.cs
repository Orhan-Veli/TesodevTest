using FluentValidation;
using Orders.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Validation
{
    public class UpdateOrderCommandRequestDtoValidation : AbstractValidator<UpdateOrderCommandRequestDto>
    {
        public UpdateOrderCommandRequestDtoValidation()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Price).NotNull().NotEmpty().NotEqual(0);
            RuleFor(x => x.ProductId).NotNull().NotEmpty().NotEqual(Guid.Empty);
            RuleFor(x => x.Quantity).NotNull().NotEmpty().NotEqual(0);
            RuleFor(x => x.Status).NotNull().NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(x => x.Country).NotEmpty().NotNull();
            RuleFor(x => x.CityCode).NotNull().NotEmpty().NotEqual(0);
            RuleFor(x => x.City).NotEmpty().NotNull();
        }
    }
}
