using ApplicationLayer.Dtos.Invoice;
using ApplicationLayer.ResultPattern;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IInvoiceService
    {
        Task<ResultT<PagedResult<GetInvoiceResponseDto>>> GetAllInvoicesAsync (GetAllInvoicesWithPaginationDto dto);

        Task<ResultT<GetInvoiceResponseDto>> GetInvoiceDetails(Guid id);
    }
}
