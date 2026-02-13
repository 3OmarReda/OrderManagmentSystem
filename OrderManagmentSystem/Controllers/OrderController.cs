using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Validation;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService _orderService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateNewOrder([FromBody] AddOrderDto dto)
        {
            if (dto is null)
                return BadRequest("Order data is required.");

            var validator = new AddOrderValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Invalid Order Data", Errors = errors });
            }

            var result = await _orderService.AddOrderAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "Order created successfully!" });
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusDto dto)
        {
            var validator = new UpdateOrderStatusValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Invalid Status Data", Errors = errors });
            }

            var result = await _orderService.UpdateOrderStatus(orderId, dto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "Order status updated successfully!" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersWithPaginationDto dto)
        {
            var validator = new GetAllOrdersWithPaginationValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Invalid Pagination Data", Errors = errors });
            }

            var result = await _orderService.GetAllOrders(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(result.Data);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            if (orderId == Guid.Empty)
                return BadRequest("Invalid OrderId.");

            var result = await _orderService.GetOrderByIdAsync(orderId);

            if (!result.IsSuccess)
                return NotFound(result.Error.Message);

            return Ok(result.Data);
        }


    }
}
