using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dto
{
    public class GetAllOrderCommandResponseDto
    {
        public List<GetOrderCommandResponseDto> Orders { get; set; }
    }
}
