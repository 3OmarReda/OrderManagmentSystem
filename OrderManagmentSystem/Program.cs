
using ApplicationLayer.Interfaces;
using ApplicationLayer.Mapping;
using ApplicationLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Data.Contracts;
using DataAccessLayer.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Middlewares;

namespace OrderManagmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });
            builder.Services.AddScoped<TransactionMiddleware>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IEmailService, SmtpEmailService>();
            builder.Services.AddAutoMapper(typeof(OrderProfileDto).Assembly);
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
