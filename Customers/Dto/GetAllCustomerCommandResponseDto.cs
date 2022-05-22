
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dto
{
    public class GetAllCustomerCommandResponseDto
    {
        public List<GetCustomerCommandResponseDto> Customers { get; set; }
    }
}
