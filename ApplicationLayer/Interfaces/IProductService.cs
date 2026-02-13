using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.Dtos.Products;
using ApplicationLayer.ResultPattern;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IProductService
    {
        Task<ResultT<PagedResult<GetProductResponseDto>>> GetAllProducts(GetAllProductsWithPaginationDto dto);

        Task<ResultT<GetProductResponseDto>> GetProductByIdAsync(Guid id);

        Task<Result> AddProductAsync(AddProductDto dto);

        Task<Result> UpdateProductAsync(Guid productId, UpdateProductDto dto);
    }

}
