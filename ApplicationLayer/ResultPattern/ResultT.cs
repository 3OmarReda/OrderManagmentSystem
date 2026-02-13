using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ResultPattern
{
    public class ResultT<T> : Result where T : class
    {
        public T Data { get; }

        private ResultT(bool isSuccess , T value , Error error) : base(isSuccess , error)
        {
            Data = value;
        }

        public static ResultT<T> Success(T data)
        => new ResultT<T>(true, data, Error.None);
        public static ResultT<T> Failure(Error error)
        => new ResultT<T>(false, default!, error);
    }
}
