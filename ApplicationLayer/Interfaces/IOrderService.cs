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
        Task<ResultT<PagedResult<GetOrderResponseDto>>> GetAllOrders(GetAllOrdersWithPaginationDto dto);
        Task<ResultT<GetOrderResponseDto>> GetOrderByIdAsync(Guid id);
        Task<Result> AddOrderAsync(AddOrderDto dto);
        Task<Result> UpdateOrderStatus(Guid orderId, UpdateOrderStatusDto dto);
    }
}
