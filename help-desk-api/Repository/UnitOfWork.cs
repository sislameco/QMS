using Microsoft.EntityFrameworkCore.Storage;
using Models.Entities;
using Repository.Db;
using Utils.LoginData;

namespace Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<T, TId> Repository<T, TId>()
            where T : BaseEntity<TId>;
        IGenericNonBaseRepository<T, TId> WithOutRepository<T, TId>() where T : class;
        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelpDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;
        private readonly IUserInfos _userInfos;
        public UnitOfWork(HelpDbContext context, IUserInfos userInfos)
        {
            _context = context;
            _userInfos = userInfos;
        }

        public IGenericRepository<T, TId> Repository<T, TId>() where T : BaseEntity<TId>
        {
            var type = typeof(T);
            if (_repositories.TryGetValue(type, out var repo))
                return (IGenericRepository<T, TId>)repo;

            var genericRepo = new GenericRepository<T, TId>(_context, _userInfos);
            _repositories[type] = genericRepo;
            return genericRepo;
        }

        public IGenericNonBaseRepository<T, TId> WithOutRepository<T, TId>() where T : class
        {
            var type = typeof(T);
            if (_repositories.TryGetValue(type, out var repo))
                return (IGenericNonBaseRepository<T, TId>)repo;

            var genericRepo = new GenericNonBaseRepository<T, TId>(_context);
            _repositories[type] = genericRepo;
            return genericRepo;
        }

        public Task<int> CommitAsync() => _context.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public Task CommitTransactionAsync() => _transaction?.CommitAsync() ?? Task.CompletedTask;

        public Task RollbackTransactionAsync() => _transaction?.RollbackAsync() ?? Task.CompletedTask;
    }
}
