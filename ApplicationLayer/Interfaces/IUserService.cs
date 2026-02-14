using ApplicationLayer.Dtos.User;
using ApplicationLayer.ResultPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IUserService
    {
        Task<Result> RegisterAsync(RegisterDto dto);
        Task<ResultT<string>> LoginAsync(DtoLogin dto);
    }

}
