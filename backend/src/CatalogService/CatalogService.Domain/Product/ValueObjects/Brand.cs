using CatalogService.Domain.Category;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product.ValueObjects;

public record Brand
{
    public string Value { get; }

    private Brand(string value)
    {
        Value = value;
    }

    public static Result<Brand, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("product.brand.empty", "Brand name cannot be empty.");

        var trimmedName = name.Trim();

        if (trimmedName.Length < Constants.Length2 || trimmedName.Length > Constants.Length100)
            return Error.Validation("product.brand.invalid.length", $"Brand name must be between {Constants.Length2} and {Constants.Length100} characters.");

        return new Brand(trimmedName);
    }

    public static implicit operator string(Brand brand) => brand.Value;
}
