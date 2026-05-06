using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Category;
public class Category
{
    // Empty constructor for EF Core
    private Category() { }

    // Full constructor for internal use
    private Category(CategoryId id, CategoryName name, string? description, CategoryId? parentCategoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
    }

    public CategoryId Id { get; private set; } = default!;
    public CategoryName Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public CategoryId? ParentCategoryId { get; private set; }

    public static Result<Category, Error> Create(
        CategoryId id,
        CategoryName name,
        string? description,
        CategoryId? parentCategoryId = null)
    {
        if (description?.Length > 500)
            return Error.Validation("category.description.too.long", "Category description cannot exceed 500 characters.");

        return new Category(id, name, description, parentCategoryId);
    }
}

