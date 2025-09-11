using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository.Db;
using System.Linq.Expressions;

namespace Repository
{
    public interface IGenericRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T?> GetByIdAsync(TId id);
        Task<List<T>> GetAllAsync(); // Added for entity list
        Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> selector);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task AddAsync(T entity);
        Task AddAndSaveAsync(T entity);
        Task UpdateAsync(T entity); // Changed to async for consistency
        void Update(T entity); // Keep for compatibility
        Task SoftDeleteAsync(T entity);
        Task DeleteAsync(TId id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
    }

    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly HelpDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(HelpDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TId id) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted);

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> selector)
        {
            return await _dbSet
                .Where(e => !e.IsDeleted)
                .Select(selector)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync();

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Where(e => !e.IsDeleted);
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
        }

        public async Task AddAndSaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await SoftDeleteAsync(entity);
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate);

        public async Task<int> CountAsync() =>
            await _dbSet.CountAsync(e => !e.IsDeleted);
    }
}
