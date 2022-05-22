using Orders.Utilities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Utilities.Concrete
{
    public class Result<T> : IResult<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
        public Result(string message, bool success, T data)
        {
            Message = message;
            Success = success;
            Data = data;
        }
        public Result(bool success, string message)
        {
            Message = message;
            Success = success;
        }
        public Result(bool success, T data)
        {
            Success = success;
            Data = data;
        }
        public Result(bool success)
        {
            Success = success;
        }
    }
}
