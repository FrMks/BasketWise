using CatalogService.Domain.Category;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product.ValueObjects;

public record ProductName
{
    public string Value { get; }

    private ProductName(string value)
    {
        Value = value;
    }

    public static Result<ProductName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("product.name.empty", "Product name cannot be empty.");

        var trimmedName = name.Trim();

        if (trimmedName.Length < Constants.Length2 || trimmedName.Length > Constants.Length250)
            return Error.Validation("product.name.invalid.length", $"Product name must be between {Constants.Length2} and {Constants.Length250} characters.");

        return new ProductName(trimmedName);
    }

    public static implicit operator string(ProductName productName) => productName.Value;
}
