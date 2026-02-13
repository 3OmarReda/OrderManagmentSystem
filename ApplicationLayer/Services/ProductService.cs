using ApplicationLayer.Dtos.Products;
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
    public class ProductService(IGenericRepository<Product> _productRepository,IMapper _mapper) : IProductService
    {
        public async Task<ResultT<PagedResult<GetProductResponseDto>>> GetAllProducts(GetAllProductsWithPaginationDto dto)
        {
            var products = _productRepository.GetAll();
            var totalCount = await products.CountAsync();
            var data = products.ProjectTo<GetProductResponseDto>(_mapper.ConfigurationProvider);
            var items = await data.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
            var pagedResult = new PagedResult<GetProductResponseDto>
            {
                Items = items,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalCount = totalCount
            };
            return ResultT<PagedResult<GetProductResponseDto>>.Success(pagedResult);
        }
        public async Task<ResultT<GetProductResponseDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
                return ResultT<GetProductResponseDto>.Failure(new Error(ErrorCode.NotFound, "Product not exist!!"));
            var data = _mapper.Map<GetProductResponseDto>(product);
            return ResultT<GetProductResponseDto>.Success(data);
        }
        public async Task<Result> AddProductAsync(AddProductDto dto)
        {
            if (dto is null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "InvalidData"));
            var data = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(data);
            return Result.Success();
        }
        public async Task<Result> UpdateProductAsync(Guid productId, UpdateProductDto dto)
        {
            if (dto is null)
                return Result.Failure(
                    new Error(ErrorCode.InvalidData, "Invalid Product Data"));

            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                return Result.Failure(
                    new Error(ErrorCode.NotFound, "Product not found"));

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.Stock;

            await _productRepository.UpdateAsync(product);

            return Result.Success("Product updated successfully");
        }
    }
}
