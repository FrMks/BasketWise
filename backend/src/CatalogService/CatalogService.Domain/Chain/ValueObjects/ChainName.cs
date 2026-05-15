using CatalogService.Domain.Category;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Chain.ValueObjects;

public record ChainName
{
    public string Value { get; }

    private ChainName(string value)
    {
        Value = value;
    }

    public static Result<ChainName, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("chain.name.empty", "Chain name cannot be empty.");

        var trimmedName = name.Trim();

        if (trimmedName.Length < Constants.Length2 || trimmedName.Length > Constants.Length100)
            return Error.Validation("chain.name.invalid.length", $"Chain name must be between {Constants.Length2} and {Constants.Length100} characters.");

        return new ChainName(trimmedName);
    }

    public static implicit operator string(ChainName chainName) => chainName.Value;
}
