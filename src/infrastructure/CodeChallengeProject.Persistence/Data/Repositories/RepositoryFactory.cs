using CodeChallengeProject.Persistence.Data.Contexts;
using CodeChallengeProject.Persistence.Data.Repositories.Interfaces;

namespace CodeChallengeProject.Persistence.Data.Repositories;

/// <inheritdoc cref="IRepositoryFactory"/>
public class RepositoryFactory : IRepositoryFactory
{
    #region Fields

    private readonly AppDbContext _context;

    #endregion

    #region Ctor

    public RepositoryFactory(AppDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    /// <inheritdoc />
    public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return new BaseRepository<TEntity>(_context);
    }

    #endregion
}