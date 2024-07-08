using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeChallengeProject.Persistence.Data.Repositories.Interfaces;

/// <summary>
///     Interface for a generic repository providing basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    ///     Asynchronously checks if any entities satisfy the specified condition.
    /// </summary>
    /// <param name="predicate">Condition to test entities.</param>
    /// <returns>True if any entities satisfy the condition; otherwise, false.</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    ///     Asynchronously retrieves an entity by its ID from the repository.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, yielding a nullable <typeparamref name="TEntity"/>.</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    ///     Asynchronously adds an entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to be added.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    ///     Asynchronously updates an entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    ///     Asynchronously saves changes made to the repository.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SaveChangesAsync();

    /// <summary>
    ///     Begins a new transaction asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation. The result contains the database transaction.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}