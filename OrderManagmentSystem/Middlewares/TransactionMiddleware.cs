using DataAccessLayer.Data;

namespace PresentationLayer.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;

        public TransactionMiddleware(AppDbContext context)
        {
            this._context = context;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await next(context);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
