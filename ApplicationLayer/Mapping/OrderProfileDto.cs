using ApplicationLayer.Dtos.Orders;
using AutoMapper;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mapping
{
    public class OrderProfileDto : Profile
    {
        public OrderProfileDto()
        {
            CreateMap<Order, GetOrderResponceDto>();
        }
    }
}
