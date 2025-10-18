using System.Linq.Expressions;

namespace HubVision.Domain.Core.Data;

public interface IRepository<T, TKey> : IDisposable where T : class
{
    IReadOnlyList<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy);
    Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy);
    IReadOnlyList<T> Get(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    T? GetById(TKey id);
    Task<T?> GetByIdAsync(TKey id);
    void Add(T entity);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    IUnitOfWork UnitOfWork { get; }
}
