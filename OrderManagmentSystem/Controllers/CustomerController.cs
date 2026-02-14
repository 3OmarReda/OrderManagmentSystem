using ApplicationLayer.Dtos.Customers;
using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Validation;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService _customerService) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerDto dto)
        {
            if (dto is null)
                return BadRequest("Customer data is required.");

            var validator = new AddCustomerValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid Customer Data",
                    Errors = errors
                });
            }

            var result = await _customerService.AddCustomerAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "Customer created successfully!" });
        }


      
        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetOrdersForCustomer(
            Guid customerId,
            [FromQuery] GetAllOrdersWithPaginationDto dto)
        {
            if (customerId == Guid.Empty)
                return BadRequest("Invalid Customer Id.");

            var paginationValidator = new GetAllOrdersWithPaginationValidator();
            var validationResult = await paginationValidator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid Pagination Data",
                    Errors = errors
                });
            }

            var result = await _customerService
                .GetOrdersForCustomerAsync(customerId, dto);

            if (!result.IsSuccess)
                return NotFound(result.Error.Message);

            return Ok(result.Data);
        }
    }
}
