using CatalogService.Domain.Chain.ValueObjects;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Chain;

public class Chain
{
    private Chain() { }

    private Chain(ChainId id, ChainName name, string? logoUrl)
    {
        Id = id;
        Name = name;
        LogoUrl = logoUrl;
    }

    public ChainId Id { get; private set; } = default!;
    public ChainName Name { get; private set; } = default!;
    public string? LogoUrl { get; private set; }

    public static Result<Chain, Error> Create(ChainId id, ChainName name, string? logoUrl = null)
    {
        return new Chain(id, name, logoUrl);
    }

    public Result<Chain, Error> Update(ChainName name, string? logoUrl)
    {
        Name = name;
        LogoUrl = logoUrl;

        return this;
    }
}
