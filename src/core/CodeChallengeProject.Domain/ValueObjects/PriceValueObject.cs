using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;

namespace CodeChallengeProject.Domain.ValueObjects;

/// <summary>
///     Represents a monetary price value object.
/// </summary>
public sealed record PriceValueObject 
{
    // Fields
    private const string Name = "Price";
    public const int MinimumPrice = 0;
    public const int MaximumPrice = 10_000_000; // Assume 10 M Tomans

    // Iranian money
    public int Value { get; }

    // Constructor
    public PriceValueObject(int value)
    {
        Validate(value);
        Value = value;
    }

    // Methods
    private static void Validate(int value)
    {
        if (value is int.MinValue or int.MaxValue)
            throw new ArgumentOutOfRangeException(paramName: Name, message: StringStaticFunctions.FormatMessage(ErrorMessages.ValueCannotBeEmptyMsg, Name));
    }
}