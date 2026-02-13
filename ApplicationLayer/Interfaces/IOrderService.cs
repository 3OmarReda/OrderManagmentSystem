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
    public interface IOrderService
    {
        Task<ResultT<PagedResult<GetOrderResponceDto>>> GetAllOrders(GetAllOrdersWithPaginationDto dto);
        Task<ResultT<GetOrderResponceDto>> GetOrderByIdAsync(Guid id);
        Task<Result> AddOrderAsync(AddOrderDto dto);
        Task<Result> UpdateOrderStatus(Guid orderId, UpdateOrderStatusDto dto);
    }
}
