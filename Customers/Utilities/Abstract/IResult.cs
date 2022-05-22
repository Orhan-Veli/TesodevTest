﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customers.Utilities.Abstract
{
    public interface IResult<T>
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }
    }
}
