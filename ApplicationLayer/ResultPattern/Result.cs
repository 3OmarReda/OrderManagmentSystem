using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ResultPattern
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Message { get; }
        public Error Error { get; }
       
        protected Result(bool isSuccess ,Error error )
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        => new Result(true, Error.None);
        public static Result Success(string message)
        => new Result(true, Error.None);
        public static Result Failure(Error error)
        => new Result(false, error);
    }
}
