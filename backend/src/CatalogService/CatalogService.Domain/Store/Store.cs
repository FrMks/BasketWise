using CatalogService.Domain.Chain;
using CSharpFunctionalExtensions;
using Shared;
using CatalogService.Domain.Store.ValueObjects;

namespace CatalogService.Domain.Store;

public sealed class Store
{
    private Store() { }

    private Store(StoreId id, string name, ChainId chainId, Location location)
    {
        Id = id;
        Name = name;
        ChainId = chainId;
        Location = location;
    }

    public StoreId Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public ChainId ChainId { get; private set; } = default!;

    public Location Location { get; private set; } = default!;

    public static Result<Store, Error> Create(StoreId id, string name, ChainId chainId, Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Error.Validation("store.name.empty", "Store name cannot be empty.");

        if (chainId == ChainId.Empty())
            return Error.Validation("store.chain.id.empty", "Chain identifier cannot be empty.");

        return new Store(id, name, chainId, location);
    }
}