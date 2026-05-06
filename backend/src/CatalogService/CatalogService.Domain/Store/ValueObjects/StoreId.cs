namespace CatalogService.Domain.Store.ValueObjects;

public record StoreId
{
    private StoreId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static StoreId NewStoreId() => new(Guid.NewGuid());

    public static StoreId Empty() => new(Guid.Empty);

    public static StoreId FromValue(Guid value) => new(value);

    public static implicit operator Guid(StoreId storeId) => storeId.Value;
}
