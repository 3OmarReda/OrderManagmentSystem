using ApplicationLayer.Dtos.Invoice;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class InvoiceController(IInvoiceService _invoiceService) : ControllerBase
{
   
    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetInvoiceById(Guid invoiceId)
    {
        if (invoiceId == Guid.Empty)
            return BadRequest("Invalid Invoice Id.");

        var result = await _invoiceService.GetInvoiceDetails(invoiceId);

        if (!result.IsSuccess)
            return NotFound(result.Error.Message);

        return Ok(result.Data);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllInvoices(
        [FromQuery] GetAllInvoicesWithPaginationDto dto)
    {
        var result = await _invoiceService.GetAllInvoicesAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(result.Error.Message);

        return Ok(result.Data);
    }
}
