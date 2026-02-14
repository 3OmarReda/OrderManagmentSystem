using ApplicationLayer.Dtos.Invoice;
using AutoMapper;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mapping
{
    public class InvoiceProfileDto : Profile
    {
        public InvoiceProfileDto()
        {
            CreateMap<Invoice, GetInvoiceResponseDto>();
        }
    }
}
