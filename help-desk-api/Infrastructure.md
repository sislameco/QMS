# API Development Coding Guidelines with Unit of Work

## 1. Project Structure

- **Controllers**: Handle HTTP requests, validation, and response formatting. Should be thin and delegate business logic to Managers.
- **Services (Services)**: Contain business logic, orchestrate calls to Unit of Work and Generic Repositories, and handle exceptions. No direct EF Core code here.
- **Unit of Work (UoW)**: Manages access to repositories and database transactions. Provides a single point to commit changes.
- **Generic Repository**: A reusable repository that supports CRUD for any entity inheriting from `BaseEntity<TId>`.
- **Models**: Entity/Domain Models and DTOs. Entity/Domain Models inherit `BaseEntity<TId>`. DTOs for input/output.

---

## 2. Naming Conventions

- **Controllers**: Plural nouns, e.g., `UsersController`.
- **Managers**: Singular noun with `Manager` suffix, e.g., `UserManager`.
- **Unit of Work**: `IUnitOfWork` and `UnitOfWork`.
- **Generic Repository**: `IGenericRepository<T, TId>` and `GenericRepository<T, TId>`.
- **DTOs**: End with `InputDto`, `OutputDto`, or `Dto`.

---

## 3. Dependency Injection

- Use constructor injection for all dependencies (UnitOfWork, Managers).
- Register all interfaces and implementations in the DI container:
  - `IUnitOfWork` → `UnitOfWork`
  - `IGenericRepository<T, TId>` → `GenericRepository<T, TId>` (resolved via UoW)
- Remove unused dependencies when refactoring.

---

## 4. Method Structure and Documentation

- Use XML comments for all public methods, including:
  - Summary of purpose
  - Numbered steps for logic
  - Exception conditions
  - Return type

Example:
```csharp
/// <summary>
/// Authenticates a user and returns authentication details.
/// <para>Steps:</para>
/// <list type="number">
/// <item>Encrypt password.</item>
/// <item>Retrieve user by credentials.</item>
/// <item>Validate user existence and role.</item>
/// </list>
/// </summary>
Task<AuthOutputDto> SignIn(AuthInputDto model);
```

---

## 5. Error Handling

- Throw custom exceptions (e.g., `BadRequestException`, `NotFoundException`) for business rule violations or not found cases.
- Use global exception middleware for consistent error responses.

---

## 6. Asynchronous Programming

- Use `async`/`await` for all I/O-bound operations (database, network).
- Unit of Work and Repository methods must be asynchronous.

---

## 7. Data Transfer Objects (DTOs)

- Always use DTOs for input/output at controller level.
- Never expose Entity models directly.

---

## 8. Validation

- Validate input DTOs in controllers using Data Annotations or FluentValidation.
- Validate business rules in Managers.

---

## 9. Pagination

- Standardize pagination DTOs for input/output.
- Always return `totalCount` and `data`.

---

## 10. Comments & Readability

- Use inline comments for non-obvious logic.
- Keep methods small and focused on a single responsibility.

---

## 11. Consistency

- Follow pattern: Controller → Manager → UnitOfWork → GenericRepository.
- Same naming, error handling, and documentation style across all features.

---

## 12. Code Cleanliness

- Remove unused `using` statements, DI registrations, and constructor parameters.
- Keep repositories and services clean of unnecessary logic.

---

## 13. Unit of Work Pattern

### Interface
```csharp
public interface IUnitOfWork
{
    IGenericRepository<T, TId> Repository<T, TId>() where T : BaseEntity<TId>;
    Task<int> CommitAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
```

### Implementation
```csharp
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Dictionary<Type, object> _repositories = new();
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IGenericRepository<T, TId> Repository<T, TId>() where T : BaseEntity<TId>
    {
        var type = typeof(T);
        if (_repositories.TryGetValue(type, out var repo))
            return (IGenericRepository<T, TId>)repo;

        var genericRepo = new GenericRepository<T, TId>(_context);
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
```

---

## 14. Manager Example

```csharp
public class UserManager : IUserManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserOutputDto> CreateUserAsync(UserInputDto dto)
    {
        var userRepo = _unitOfWork.Repository<User, Guid>();

        var existing = await userRepo.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (existing != null)
            throw new BadRequestException("Email already exists");

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            RoleId = dto.RoleId
        };

        await userRepo.AddAsync(user);
        await _unitOfWork.CommitAsync();

        return new UserOutputDto { Id = user.Id, Name = user.Name, Email = user.Email };
    }
}
```

---

## 15. Transaction Example

```csharp
public async Task AssignRoleAsync(Guid userId, int roleId)
{
    await _unitOfWork.BeginTransactionAsync();

    try
    {
        var userRepo = _unitOfWork.Repository<User, Guid>();
        var roleRepo = _unitOfWork.Repository<Role, int>();

        var user = await userRepo.GetByIdAsync(userId);
        var role = await roleRepo.GetByIdAsync(roleId);

        if (user == null || role == null)
            throw new NotFoundException("User or Role not found");

        user.RoleId = role.Id;
        userRepo.Update(user);

        await _unitOfWork.CommitAsync();
        await _unitOfWork.CommitTransactionAsync();
    }
    catch
    {
        await _unitOfWork.RollbackTransactionAsync();
        throw;
    }
}
```

---

**✅ Follow this guideline for all new code and refactoring.**
