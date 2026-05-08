using CatalogService.Domain.Category;
using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product.ValueObjects;

public record Barcode
{
    public string Value { get; init; }

    private Barcode(string value)
    {
        Value = value;
    }

    public static Result<Barcode, Error> Create(string value)
    {
        if (value.Length != Constants.Length13)
            return Error.Validation("barcode.not.correct.lenght", "Barcode should be 13 digits.");

        if (!value.All(char.IsDigit))
            return Error.Validation("barcode.not.digit", "Barcode should be with only digit");

        return new Barcode(value);
    }
}