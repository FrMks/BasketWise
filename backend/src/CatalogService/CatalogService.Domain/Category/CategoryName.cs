using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Category;

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

        if (trimmedName.Length < CategoryConstraints.Length2 || trimmedName.Length > CategoryConstraints.Length100)
            return Error.Validation("category.name.invalid.length", $"Category name must be between {CategoryConstraints.Length2} and {CategoryConstraints.Length100} characters.");

        return new CategoryName(trimmedName);
    }

    public static implicit operator string(CategoryName categoryName) => categoryName.Value;
}
