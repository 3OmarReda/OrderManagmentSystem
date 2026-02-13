using ApplicationLayer.Dtos.Products;
using AutoMapper;
using DataAccessLayer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Mapping
{
    public class ProductDtoProfile : Profile
    {
        public ProductDtoProfile()
        {
            CreateMap<Product, GetProductResponseDto>();
            CreateMap<AddProductDto, Product>();
        }
    }
}
