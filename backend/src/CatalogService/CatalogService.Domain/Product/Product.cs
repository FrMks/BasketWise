using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Domain.Product.ValueObjects;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product;

public class Product
{
    private Product() {}

    private Product(Guid id, string name, string brand, Barcode barcode, Guid categoryId, string? description)
    {
        Id = id;
        Name = name;
        Brand = brand;
        Barcode = barcode;
        CategoryId = categoryId;
        Description = description;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Brand { get; private set; }

    public Barcode Barcode { get; private set; }

    public Guid CategoryId { get; private set; }

    public string? Decription { get; private set; }

    // Factory method - This is where business rules live
    public static Result<Product> Create(
        string name,
        string brand,
        Barcode barcode,
        Guid categoryId,
        string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Product>("Product name cannot be empty.");

        if (string.IsNullOrWhiteSpace(brand))
            return Result.Failure<Product>("Brand cannot be empty.");

        var product = new Product(Guid.NewGuid(), name, brand, barcode, categoryId, description);
        
        return Result.Success(product);
    }
}
