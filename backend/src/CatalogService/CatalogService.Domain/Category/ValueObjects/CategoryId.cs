namespace CatalogService.Domain.Category.ValueObjects;

public record CategoryId
{
    private CategoryId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CategoryId NewCategoryId() => new(Guid.NewGuid());

    public static CategoryId Empty() => new(Guid.Empty);

    public static CategoryId FromValue(Guid value) => new(value);

    public static implicit operator Guid(CategoryId categoryId) => categoryId.Value;
}
