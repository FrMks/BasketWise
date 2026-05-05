using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Chain;

public class Chain
{
    private Chain() { }

    private Chain(Guid id, string name, string? logoUrl)
    {
        Id = id;
        Name = name;
        LogoUrl = logoUrl;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string? LogoUrl { get; private set; }

    public static Result<Chain, Error> Create(Guid id, string name, string? logoUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("chain.name.empty", "Chain name cannot be empty.");

        if (name.Length > 100)
            return Error.Validation("chain.name.too.long", "Chain name cannot exceed 100 characters.");

        return new Chain(id, name, logoUrl);
    }

    public Result<Chain, Error> Update(string name, string? logoUrl)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("chain.name.empty", "Chain name cannot be empty.");

        Name = name;
        LogoUrl = logoUrl;

        return this;
    }
}
