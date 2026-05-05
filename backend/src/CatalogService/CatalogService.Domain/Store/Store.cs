using CSharpFunctionalExtensions;
using Shared;
using CatalogService.Domain.Store.ValueObjects;

namespace CatalogService.Domain.Store;

public sealed class Store
{
    private Store() { }

    private Store(Guid id, string name, Guid chainId, Location location)
    {
        Id = id;
        Name = name;
        ChainId = chainId;
        Location = location;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public Guid ChainId { get; private set; }

    public Location Location { get; private set; } = default!;

    public static Result<Store, Error> Create(Guid id, string name, Guid chainId, Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("store.name.empty", "Store name cannot be empty.");

        if (chainId == Guid.Empty)
            return Error.Validation("store.chain.id.empty", "Chain identifier cannot be empty.");

        return new Store(id, name, chainId, location);
    }
}