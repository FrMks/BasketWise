using CSharpFunctionalExtensions;
using Shared;
using CatalogService.Domain.Store.ValueObjects;

namespace CatalogService.Domain.Store;

public sealed class Store
{
    private Store() { }

    private Store(Guid id, string name, string chainName, Location location)
    {
        Id = id;
        Name = name;
        ChainName = chainName;
        Location = location;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string ChainName { get; private set; }

    public Location Location { get; private set; } = default!;

    public static Result<Store, Error> Create(Guid id, string name, string chainName, Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("store.name.empty", "Store name cannot be empty.");

        if (string.IsNullOrWhiteSpace(chainName))
            return Error.Validation("store.chain.name.empty", "Chain name cannot be empty.");

        return new Store(id, name, chainName, location);
    }
}