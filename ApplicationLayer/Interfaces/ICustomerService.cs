using ApplicationLayer.Dtos.Customers;
using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.ResultPattern;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface ICustomerService
    {
        Task<Result> AddCustomerAsync(AddCustomerDto dto);

        Task<ResultT<PagedResult<GetOrderResponseDto>>>
            GetOrdersForCustomerAsync(Guid customerId, GetAllOrdersWithPaginationDto dto);
    }


}
