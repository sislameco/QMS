using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enum;
using Repository.Db;
using System.Linq.Expressions;
using Utils.LoginData;

namespace Repository
{
    public interface IGenericRepository<T, TId> where T : BaseEntity<TId>
    {
        Task<T?> GetByIdAsync(TId id);
        Task<List<T>> GetAllAsync(); // Added for entity list
        Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> selector);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate,
         params Expression<Func<T, object>>[] includes);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        Task AddAsync(T entity);
        Task AddAndSaveAsync(T entity);
        Task UpdateAsync(T entity); // Changed to async for consistency
        void Update(T entity); // Keep for compatibility
        Task SoftDeleteAsync(T entity);
        Task DeleteAsync(TId id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        List<TResult> FindByConditionOneColumn<TResult>(
           Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector);
        Task BulkInsertAsync(IEnumerable<T> entities);
    }

    public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T : BaseEntity<TId>
    {
        protected readonly HelpDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IUserInfos _userInfos;
         public GenericRepository(HelpDbContext context, IUserInfos userInfos)
        {
            _userInfos = userInfos;
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(TId id) =>
            await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id) && e.RStatus == EnumRStatus.Active);

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.RStatus == EnumRStatus.Active).ToListAsync();
        }

        public async Task<List<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> selector)
        {
            return await _dbSet
                .Where(e => e.RStatus == EnumRStatus.Active)
                .Select(selector)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(
         Expression<Func<T, bool>> predicate,
         params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet
                .Where(predicate)
                .Where(e => e.RStatus == EnumRStatus.Active);

            // Apply eager loading includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.Where(e => e.RStatus == EnumRStatus.Active);
            var total = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return (items, total);
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.RStatus = EnumRStatus.Active;
            entity.CreatedBy = _userInfos.GetCurrentUserId();
            await _dbSet.AddAsync(entity);
        }

        public async Task AddAndSaveAsync(T entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.RStatus = EnumRStatus.Active;
            entity.CreatedBy = _userInfos.GetCurrentUserId();
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = _userInfos.GetCurrentUserId();
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            entity.UpdatedBy = _userInfos.GetCurrentUserId();
            _dbSet.Update(entity);
        }

        public async Task SoftDeleteAsync(T entity)
        {
            entity.RStatus = Models.Enum.EnumRStatus.Deleted;
            entity.DeletedDate = DateTime.UtcNow;
            entity.DeletedBy = _userInfos.GetCurrentUserId();
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
            await _dbSet.Where(e => e.RStatus == EnumRStatus.Active).AnyAsync(predicate);

        public async Task<int> CountAsync() =>
            await _dbSet.CountAsync(e => e.RStatus == EnumRStatus.Active);

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).Where(e => e.RStatus == EnumRStatus.Active).FirstOrDefaultAsync();
        }

        public List<TResult> FindByConditionOneColumn<TResult>(
           Expression<Func<T, bool>> predicate,
           Expression<Func<T, TResult>> selector)
        {
            return _dbSet
                .Where(predicate)
                .Where(e => e.RStatus == EnumRStatus.Active)
                .Select(selector)
                .ToList();
        }
        public async Task BulkInsertAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
    }
}
