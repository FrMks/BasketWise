namespace CatalogService.Domain.Product;

public record ProductId
{
    private ProductId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProductId NewProductId() => new(Guid.NewGuid());

    public static ProductId Empty() => new(Guid.Empty);

    public static ProductId FromValue(Guid value) => new(value);

    public static implicit operator Guid(ProductId productId) => productId.Value;
}
