using CatalogService.Domain.Category;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Store.ValueObjects;

public record StoreName
{
    public string Value { get; }

    private StoreName(string value)
    {
        Value = value;
    }

    public static Result<StoreName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("store.name.empty", "Store name cannot be empty.");

        var trimmedName = name.Trim();

        if (trimmedName.Length < Constants.Length2 || trimmedName.Length > Constants.Length100)
            return Error.Validation("store.name.invalid.length", $"Store name must be between {Constants.Length2} and {Constants.Length100} characters.");

        return new StoreName(trimmedName);
    }

    public static implicit operator string(StoreName storeName) => storeName.Value;
}
