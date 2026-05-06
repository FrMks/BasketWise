using CatalogService.Domain.Category;
using CatalogService.Domain.Product.ValueObjects;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product;

public class Product
{
    private Product() { }

    private Product(
        ProductId id,
        string name,
        string brand,
        Barcode barcode,
        Measurement measurement,
        CategoryId categoryId,
        string? description)
    {
        Id = id;
        Name = name;
        Brand = brand;
        Barcode = barcode;
        Measurement = measurement;
        CategoryId = categoryId;
        Description = description;
    }

    public ProductId Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Brand { get; private set; } = default!;

    public Barcode Barcode { get; private set; } = default!;

    public Measurement Measurement { get; private set; } = default!;

    public CategoryId CategoryId { get; private set; } = default!;

    public string? Description { get; private set; }

    public static Result<Product, Error> Create(
        ProductId id,
        string name,
        string brand,
        Barcode barcode,
        Measurement measurement,
        CategoryId categoryId,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("product.name.empty", "Product name cannot be empty.");

        if (string.IsNullOrWhiteSpace(brand))
            return Error.Validation("product.brand.empty", "Brand cannot be empty.");

        return new Product(id, name, brand, barcode, measurement, categoryId, description);
    }
}
