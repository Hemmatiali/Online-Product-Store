using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;

namespace CodeChallengeProject.Domain.ValueObjects;

/// <summary>
///     Represents an inventory count value object.
/// </summary>
public sealed record InventoryCountValueObject
{
    // Fields
    private const string Name = "Inventory Count";
    public const int MinimumCount = 0;
    public int Value { get; private set; }

    // Constructor
    public InventoryCountValueObject(int value)
    {
        Validate(value);
        Value = value;
    }

    // Methods
    private static void Validate(int value)
    {
        if (value is int.MinValue or < MinimumCount)
            throw new ArgumentOutOfRangeException(paramName: Name, message: StringStaticFunctions.FormatMessage(ErrorMessages.MinMsg, Name, MinimumCount));
    }
}