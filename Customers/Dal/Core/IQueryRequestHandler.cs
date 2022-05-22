using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Dal.Core
{
    public interface IQueryRequestHandler<TRequest,TResponse> : IRequestHandler<TRequest,TResponse> where TRequest : IQueryRequest<TResponse>
    {
    }
}
