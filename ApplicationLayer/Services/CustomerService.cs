using ApplicationLayer.Dtos.Customers;
using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.Interfaces;
using ApplicationLayer.ResultPattern;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer.Data.Contracts;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Helpers;

namespace ApplicationLayer.Services
{
    public class CustomerService(IGenericRepository<Customer> _customerRepository, IMapper _mapper) : ICustomerService
    {
        public async Task<Result> AddCustomerAsync(AddCustomerDto dto)
        {
            if (dto is null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Customer is empty"));
            var data = _mapper.Map<Customer>(dto);
            await _customerRepository.AddAsync(data);
            return Result.Success();
        }


        public async Task<ResultT<PagedResult<GetOrderResponseDto>>> GetOrdersForCustomerAsync(Guid customerId, GetAllOrdersWithPaginationDto dto)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer is null)
                return ResultT<PagedResult<GetOrderResponseDto>>
                    .Failure(new Error(ErrorCode.NotFound, "Customer not found"));

            var query = customer.Orders.AsQueryable(); // Orders موجودة داخل الـ Customer entity

            var totalCount = query.Count();

            var data = query.ProjectTo<GetOrderResponseDto>(_mapper.ConfigurationProvider);

            var items = await data
                .Skip((dto.PageNumber - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            // 6️⃣ عمل PagedResult
            var pagedResult = new PagedResult<GetOrderResponseDto>
            {
                Items = items,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalCount = totalCount
            };

            return ResultT<PagedResult<GetOrderResponseDto>>.Success(pagedResult);
        }

    }
}

