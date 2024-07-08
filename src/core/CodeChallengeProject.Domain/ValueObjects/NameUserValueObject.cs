using CodeChallengeProject.Domain.Shared;
using CodeChallengeProject.Domain.Shared.StaticFunctions;

namespace CodeChallengeProject.Domain.ValueObjects;

/// <summary>
///     Represents a user's name value object.
/// </summary>
public sealed record NameUserValueObject
{
    // Fields
    private const string Name = "User's name";
    public const int MinimumLength = 2;
    public const int MaximumLength = 15;
    public string Value { get; }

    // Constructor
    public NameUserValueObject(string value)
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