using ApplicationLayer.Dtos.Orders;
using ApplicationLayer.Interfaces;
using ApplicationLayer.ResultPattern;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer.Data.Contracts;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class OrderService(IEmailService _emailService, IGenericRepository<Product> _productRepository, IGenericRepository<Order> _orderRepository, IMapper _mapper, IGenericRepository<Customer> _customerRepository) : IOrderService
    {
        public async Task<ResultT<PagedResult<GetOrderResponceDto>>> GetAllOrders(GetAllOrdersWithPaginationDto dto)
        {
            var query = _orderRepository.GetAll();
            var totalCount = await query.CountAsync();
            var data = query.ProjectTo<GetOrderResponceDto>(_mapper.ConfigurationProvider);
            var items = await data.Skip((dto.PageNumber - 1) * dto.PageSize).Take(dto.PageSize).ToListAsync();
            var pagedResult = new PagedResult<GetOrderResponceDto>
            {
                Items = items,
                PageNumber = dto.PageNumber,
                PageSize = dto.PageSize,
                TotalCount = totalCount
            };
            return ResultT<PagedResult<GetOrderResponceDto>>.Success(pagedResult);
        }

        public async Task<ResultT<GetOrderResponceDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order is null)
                return ResultT<GetOrderResponceDto>.Failure(new Error(ErrorCode.NotFound, "Order Not Found !!"));
            var data = _mapper.Map<GetOrderResponceDto>(order);
            return ResultT<GetOrderResponceDto>.Success(data);
        }
        public async Task<Result> AddOrderAsync(AddOrderDto dto)
        {
            if (dto is null || dto.OrderItems is null || !dto.OrderItems.Any())
                return Result.Failure(
                    new Error(ErrorCode.InvalidData, "Invalid Order Data"));

            var customer = await _customerRepository.GetByIdAsync(dto.CustomerId);
            if (customer is null)
                return Result.Failure(
                    new Error(ErrorCode.NotFound, "Customer not found"));

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                PaymentMethod = dto.PaymentMethod,
                Status = Status.Pending,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            decimal amount = 0;

            foreach (var item in dto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);

                if (product is null)
                    return Result.Failure(
                        new Error(ErrorCode.NotFound, "Product not found"));

                if (product.Stock < item.Quantity)
                    return Result.Failure(
                        new Error(ErrorCode.InvalidData, "Insufficient Stock"));

                var subTotal = item.Quantity * product.Price;
                amount += subTotal;

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    Discount = 0
                });

                product.Stock -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            if (amount > 200)
                amount *= 0.9m;
            else if (amount > 100)
                amount *= 0.95m;

            order.TotalAmount = amount;

            order.Invoice = new Invoice
            {
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = amount
            };

            await _orderRepository.AddAsync(order);

            return Result.Success();
        }

        public async Task<Result> UpdateOrderStatus(Guid orderId, UpdateOrderStatusDto dto)
        {
            if (dto is null)
                return Result.Failure(new Error(ErrorCode.InvalidData, "Invalid data"));

            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order is null)
                return Result.Failure( new Error(ErrorCode.NotFound, "Order not found")); 
            if(!IsValidStatusTransition(order.Status,dto.Status))
                return Result.Failure(
                new Error(ErrorCode.InvalidData, "Invalid status transition"));

            order.Status = dto.Status;
            await _orderRepository.UpdateAsync(order);
            //Email
            var subject = $"Order #{order.Id} Status Updated";
            var body = $"Hello, your order status is now: {order.Status}";
            await _emailService.SendEmailAsync(order.Customer.Email, subject, body);
            return Result.Success("Successfuly Completed");
        }

        #region Helper
        private bool IsValidStatusTransition(Status current, Status next)
        {
            return current switch
            {
                Status.Pending => next == Status.Confirmed || next == Status.Cancelled,
                Status.Confirmed => next == Status.Shipped || next == Status.Cancelled,
                Status.Shipped => next == Status.Delivered,
                Status.Delivered => false,
                Status.Cancelled => false
            };
        }
        #endregion
    }
}
