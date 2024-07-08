using System.Linq.Expressions;
using CodeChallengeProject.Persistence.Data.Contexts;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeChallengeProject.Persistence.Data.Repositories;

/// <inheritdoc cref="IBaseRepository"/>
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    #region Fields

    private readonly AppDbContext _context;

    #endregion

    #region Ctor

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc/>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().AnyAsync(predicate);

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

    /// <inheritdoc/>
    public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

    /// <inheritdoc/>
    public async Task UpdateAsync(TEntity entity) => await Task.Run(() => _context.Set<TEntity>().Update(entity));

    /// <inheritdoc/>
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    /// <inheritdoc/>
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default) => await _context.Database.BeginTransactionAsync(cancellationToken);

    #endregion
}