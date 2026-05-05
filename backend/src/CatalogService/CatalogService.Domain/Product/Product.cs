using CatalogService.Domain.Product.ValueObjects;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product;

public class Product
{
    private Product() { }

    private Product(Guid id, string name, string brand, Barcode barcode, Guid categoryId, string? description)
    {
        Id = id;
        Name = name;
        Brand = brand;
        Barcode = barcode;
        CategoryId = categoryId;
        Description = description;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Brand { get; private set; }

    public Barcode Barcode { get; private set; }

    public Guid CategoryId { get; private set; }

    public string? Description { get; private set; }

    public static Result<Product, Error> Create(
        Guid id,
        string name,
        string brand,
        Barcode barcode,
        Guid categoryId,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("product.name.empty", "Product name cannot be empty.");

        if (string.IsNullOrWhiteSpace(brand))
            return Error.Validation("product.brand.empty", "Brand cannot be empty.");

        return new Product(id, name, brand, barcode, categoryId, description);
    }
}
