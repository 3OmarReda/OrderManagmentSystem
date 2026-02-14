using ApplicationLayer.Dtos.Customers;
using AutoMapper;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mapping
{
    public class CustomerProfileDto : Profile
    {
        public CustomerProfileDto()
        {
            CreateMap<AddCustomerDto, Customer>();

        }
    }
}
