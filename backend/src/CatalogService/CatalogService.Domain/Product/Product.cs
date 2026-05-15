using CatalogService.Domain.Category;
using CatalogService.Domain.Category.ValueObjects;
using CatalogService.Domain.Product.ValueObjects;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product;

public class Product
{
    private Product() { }

    private Product(
        ProductId id,
        ProductName name,
        Brand brand,
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

    public ProductName Name { get; private set; } = default!;

    public Brand Brand { get; private set; } = default!;

    public Barcode Barcode { get; private set; } = default!;

    public Measurement Measurement { get; private set; } = default!;

    public CategoryId CategoryId { get; private set; } = default!;

    public string? Description { get; private set; }

    public static Result<Product, Error> Create(
        ProductId id,
        ProductName name,
        Brand brand,
        Barcode barcode,
        Measurement measurement,
        CategoryId categoryId,
        string? description)
    {
        if (description?.Length > Constants.Length500)
            return Error.Validation("product.description.too.long", $"Product description cannot exceed {Constants.Length500} characters.");

        return new Product(id, name, brand, barcode, measurement, categoryId, description);
    }
}
