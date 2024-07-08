using CodeChallengeProject.Domain.Entities;
using CodeChallengeProject.Persistence.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CodeChallengeProject.Persistence.Data.Contexts;

/// <summary>
///     Represents the SQL database context for the CodeChallenge application.
/// </summary>
/// <remarks>
///     This DbContext is responsible for interacting with the underlying database to manage library-related data.
/// </remarks>
public class AppDbContext : DbContext
{
    #region Fields

    #endregion

    #region Ctor

    public AppDbContext()
    { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #endregion

    #region DbSets

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }

    #endregion

    #region Methods

    /// <summary>
    ///     Overrides the default behavior of building the model for the AppDbContext.
    /// </summary>
    /// <param name="modelBuilder">The ModelBuilder instance used to construct the database model.</param>
    /// <remarks>
    ///     This method allows customization of the database model, including configuring entity relationships, constraints, etc.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    #endregion
}