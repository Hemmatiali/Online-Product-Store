namespace CodeChallengeProject.Domain.Shared;

/// <summary>
///     Static class containing error messages used in the application.
/// </summary>
/// <remarks>
///     This class provides a centralized location for storing error messages used throughout the application.
/// </remarks>
public static class ErrorMessages
{
    // General messages
    public const string ItemNotFoundMsg = "{0} not found.";
    public const string InputCannotBeNullWhiteSpaceMsg = "{0} cannot contain a null or whitespace value.";
    public const string ExistMsg = "{0} is exist. try another one.";

    // Empty or invalid format
    public const string ValueCannotBeEmptyMsg = "{0} cannot be empty.";
    public const string CanNotBeNegativeMsg = "{0} cannot be negative.";

    // Validator messages
    public const string RequiredFieldMsg = "{0} is required.";
    public const string LengthBetweenMsg = "{0} must be between {1} and {2}.";
    public const string LengthCharactersBetweenMsg = "{0} must be between {1} and {2} characters.";
    public const string MaximumLengthMsg = "{0} must be a maximum of {1} characters.";
    public const string MaxMsg = "{0} cannot be more than {1}.";
    public const string MinMsg = "{0} cannot be less than {1}.";

    // Unexpected error
    public const string UnexpectedErrorMsg = "An unexpected error occurred.";
}