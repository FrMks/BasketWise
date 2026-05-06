namespace CatalogService.Domain.Chain;

public record ChainId
{
    private ChainId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ChainId NewChainId() => new(Guid.NewGuid());

    public static ChainId Empty() => new(Guid.Empty);

    public static ChainId FromValue(Guid value) => new(value);

    public static implicit operator Guid(ChainId chainId) => chainId.Value;
}
