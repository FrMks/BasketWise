using System.Data.Common;
using CatalogService.Core.Database;
using CatalogService.Domain.Category;
using CatalogService.Domain.Chain;
using CatalogService.Domain.Product;
using CatalogService.Domain.Store;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Database;

namespace CatalogService.Infrastructure.PostgreSQL;

public class CatalogServiceDbContext : DbContext, IReadDbContext, IDbConnectionFactory
{
    public CatalogServiceDbContext(DbContextOptions<CatalogServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // ВАЖНО для Модульного Монолита: отдельная схема для каждого модуля
        modelBuilder.HasDefaultSchema("catalog");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogServiceDbContext).Assembly);
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Chain> Chains => Set<Chain>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Store> Stores => Set<Store>();


    public IQueryable<Category> CategoriesRead => Set<Category>().AsQueryable().AsNoTracking();
    public IQueryable<Chain> ChainsRead => Set<Chain>().AsQueryable().AsNoTracking();
    public IQueryable<Product> ProductsRead => Set<Product>().AsQueryable().AsNoTracking();
    public IQueryable<Store> StoresRead => Set<Store>().AsQueryable().AsNoTracking();

    public DbConnection GetDbConnection() => Database.GetDbConnection();
}