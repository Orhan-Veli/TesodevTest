using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Dal.Core
{
    public interface IQueryRequest<TResponse> : IRequest<TResponse> 
    {
    }
}
