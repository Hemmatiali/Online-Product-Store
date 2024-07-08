namespace CodeChallengeProject.Persistence.Data.Repositories.Interfaces;

/// <summary>
///     A factory that creates repository instances.
/// </summary>
/// <remarks>
///     Defines a method for getting repositories for specific entity types.
/// </remarks>
public interface IRepositoryFactory
{
    IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}