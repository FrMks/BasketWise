using CatalogService.Domain.Category;
using CatalogService.Domain.Chain;
using CatalogService.Domain.Product;
using CatalogService.Domain.Store;

namespace CatalogService.Core.Database;

public interface IReadDbContext
{
    IQueryable<Category> CategoriesRead { get; }

    IQueryable<Chain> ChainsRead { get; }

    IQueryable<Product> ProductsRead { get; }

    IQueryable<Store> StoresRead { get; }
}