using ApplicationLayer.Dtos.Invoice;
using ApplicationLayer.Interfaces;
using ApplicationLayer.ResultPattern;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer.Data.Contracts;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class InvoiceService(IGenericRepository<Invoice> _invoiceRepository,IMapper _mapper) : IInvoiceService
    {
        public async Task<ResultT<PagedResult<GetInvoiceResponseDto>>> GetAllInvoicesAsync(GetAllInvoicesWithPaginationDto dto)
        {
            var query = _invoiceRepository.GetAll();
            var TotalCount = await query.CountAsync();
            var data = query.ProjectTo<GetInvoiceResponseDto>(_mapper.ConfigurationProvider);
            var items = await data.Skip((dto.PageNumber - 1) * dto.PageSize).ToListAsync();
            var pagedResult = new PagedResult<GetInvoiceResponseDto>
            {
                Items = items,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalCount = TotalCount
            };
            return ResultT<PagedResult<GetInvoiceResponseDto>>
           .Success(pagedResult);
        }

        public async Task<ResultT<GetInvoiceResponseDto>> GetInvoiceDetails(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);

            if (invoice is null)
                return ResultT<GetInvoiceResponseDto>
                    .Failure(new Error(ErrorCode.NotFound, "Invoice not found"));

            var data = _mapper.Map<GetInvoiceResponseDto>(invoice);

            return ResultT<GetInvoiceResponseDto>.Success(data);
        }
    }
}
