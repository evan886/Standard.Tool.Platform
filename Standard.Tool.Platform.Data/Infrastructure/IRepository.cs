using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Standard.Tool.Platform.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task CreateDBAsync(CancellationToken ct = default);
        Task Clear(CancellationToken ct = default);

        ValueTask<T> GetAsync(object key, CancellationToken ct = default);

        Task<T> GetAsync(Expression<Func<T, bool>> condition);

        Task<IReadOnlyList<T>> ListAsync(CancellationToken ct = default);

        IQueryable<T> AsQueryable();

        Task<int> DeleteAsync(T entity, CancellationToken ct = default);

        Task<int> DeleteAsync(CancellationToken ct = default);

        Task DeleteAsync(IEnumerable<T> entities, CancellationToken ct = default);

        Task<int> DeleteAsync(object key, CancellationToken ct = default);

        Task<int> CountAsync(ISpecification<T> spec = null, CancellationToken ct = default);

        Task<int> CountAsync(Expression<Func<T, bool>> condition, CancellationToken ct = default);

        Task<bool> AnyAsync(Expression<Func<T, bool>> condition = null, CancellationToken ct = default);

        Task<IList<TResult>> SelectAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = default);

        Task<IList<TResult>> SelectAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector);

        Task<TResult> FirstOrDefaultAsync<TResult>(ISpecification<T> spec, Expression<Func<T, TResult>> selector);

        IList<TResult> Select<TResult>(Expression<Func<T, TResult>> selector);

        Task<T> AddAsync(T entity, CancellationToken ct = default);

        Task<int> UpdateAsync(T entity, CancellationToken ct = default);

        int Update(T entity);

        Task<int> AddRangeAsync(IEnumerable<T> entity);
    }
}
