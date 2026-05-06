using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Category.ValueObjects;

public record CategoryName
{
    public string Value { get; }

    private CategoryName(string value)
    {
        Value = value;
    }

    public static Result<CategoryName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("category.name.empty", "Category name cannot be empty.");

        var trimmedName = name.Trim();

        if (trimmedName.Length < Constants.Length2 || trimmedName.Length > Constants.Length100)
            return Error.Validation("category.name.invalid.length", $"Category name must be between {Constants.Length2} and {Constants.Length100} characters.");

        return new CategoryName(trimmedName);
    }

    public static implicit operator string(CategoryName categoryName) => categoryName.Value;
}
