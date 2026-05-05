using CSharpFunctionalExtensions;
using Shared;

namespace CatalogService.Domain.Product.ValueObjects;

public enum UnitType
{
    Gram = 1,
    Kilogram = 2,
    Milliliter = 3,
    Liter = 4,
    Piece = 5
}

public sealed record Measurement
{
    public decimal Value { get; }
    public UnitType Unit { get; }

    private Measurement(decimal value, UnitType unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Result<Measurement, Error> Create(decimal value, UnitType unit)
    {
        if (value <= 0)
            return Error.Validation("measurement.value.invalid", "Measurement value must be greater than zero.");

        return new Measurement(value, unit);
    }

    /// <summary>
    /// Returns the value normalized to a base unit (Kilograms for Weight, Liters for Volume, Pieces for Count).
    /// </summary>
    public decimal GetNormalizedValue()
    {
        return Unit switch
        {
            UnitType.Gram => Value / 1000m,
            UnitType.Milliliter => Value / 1000m,
            _ => Value
        };
    }

    public string GetBaseUnitName()
    {
        return Unit switch
        {
            UnitType.Gram or UnitType.Kilogram => "kg",
            UnitType.Milliliter or UnitType.Liter => "l",
            _ => "pcs"
        };
    }
}
