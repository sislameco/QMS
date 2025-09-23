using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enum;
using Repository.Db;
using System.Linq.Expressions;
using Utils.LoginData;

namespace Repository
{
    public interface IGenericNonBaseRepository<T, TId> 
        where T : class
    {
        Task AddAsync(T entity);
        Task AddAndSaveAsync(T entity);
        Task UpdateAsync(T entity); 
        void Update(T entity); // Keep for compatibility
        Task SoftDeleteAsync(T entity);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    }

    public class GenericNonBaseRepository<T, TId> : IGenericNonBaseRepository<T, TId> 
        where T : class
    {
        protected readonly HelpDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IUserInfos _userInfos;
        public GenericNonBaseRepository(HelpDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddAndSaveAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SoftDeleteAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
