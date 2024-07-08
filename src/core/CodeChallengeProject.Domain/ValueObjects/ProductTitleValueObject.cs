using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;

namespace CodeChallengeProject.Domain.ValueObjects;

/// <summary>
///     Represents a title of product value object.
/// </summary>
public sealed record ProductTitleValueObject 
{
    // Fields
    private const string Name = "Product title";
    public const int MinimumLength = 3;
    public const int MaximumLength = 40;
    public string Value { get; }

    // Constructor
    public ProductTitleValueObject(string value)
    {
        Validate(value);
        Value = value;
    }

    // Methods
    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(paramName: Name, message: StringStaticFunctions.FormatMessage(ErrorMessages.ValueCannotBeEmptyMsg, Name));

        if (value.Length is <= MinimumLength or >= MaximumLength)
            throw new ArgumentOutOfRangeException(paramName: Name, message: StringStaticFunctions.FormatMessage(ErrorMessages.LengthCharactersBetweenMsg, Name, MinimumLength, MaximumLength));
    }
}