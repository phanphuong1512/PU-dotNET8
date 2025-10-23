using Microsoft.EntityFrameworkCore;
using PU.Repositories.Interfaces;
using PU.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PU.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteAsync(int orderId, int productId)
        {
            var detail = await _context.OrderDetails
                .FirstOrDefaultAsync(d => d.OrderId == orderId && d.ProductId == productId);

            if (detail == null)
                return false; // Nothing to delete

            _context.OrderDetails.Remove(detail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}