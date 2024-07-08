using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;

namespace CodeChallengeProject.Domain.ValueObjects;

/// <summary>
///     Represents an integer discount percentage value object.
/// </summary>
public sealed record DiscountValueObject
{
    // Fields
    private const string Name = "Discount";
    public const int MinimumDiscount = 0;
    public const int MaximumDiscount = 100;
    public int Value { get; }

    // Constructor
    public DiscountValueObject(int value)
    {
        Validate(value);
        Value = value;
    }

    // Methods
    private static void Validate(int value)
    {
        if (value is MinimumDiscount or MaximumDiscount)
            throw new ArgumentOutOfRangeException(paramName: Name, message: StringStaticFunctions.FormatMessage(ErrorMessages.LengthBetweenMsg, Name, MinimumDiscount, MaximumDiscount));
    }

    /// <summary>
    ///     Calculates the discount amount for a given original price.
    /// </summary>
    /// <param name="originalPrice">The original price.</param>
    /// <returns>The calculated discount amount.</returns>
    public int CalculateDiscountAmount(int originalPrice) => originalPrice * Value / 100;
}