using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.ResultPattern
{
    public sealed record Error(ErrorCode ErrorCode , string Message)
    {
        public static readonly Error None =
            new Error(ErrorCode.NoError, string.Empty);
    }
}
