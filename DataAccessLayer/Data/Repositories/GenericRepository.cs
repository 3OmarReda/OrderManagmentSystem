using DataAccessLayer.Data.Contracts;
using DataAccessLayer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data.Repositories
{
    public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
    {

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<T>().Update(entity);
            }
        }
    }



}
