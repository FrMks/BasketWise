using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Category;
public class Category
{
    // Empty constructor for EF Core
    private Category() { }

    // Full constructor for internal use
    private Category(CategoryId id, string name, string? description, CategoryId? parentCategoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }

    public CategoryId Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public CategoryId? ParentCategoryId { get; private set; }

    public static Result<Category, Error> Create(
        CategoryId id,
        string name,
        string? description,
        CategoryId? parentCategoryId = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("category.name.empty", "Category name cannot be empty.");

        if (name.Length > 100)
            return Error.Validation("category.name.too.long", "Category name cannot exceed 100 characters.");

        return new Category(id, name, description, parentCategoryId);
    }
}

