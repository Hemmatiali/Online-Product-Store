namespace CodeChallengeProject.Domain.Shared.StaticFunctions;

/// <summary>
///     Represents a collection of static string utility functions.
/// </summary>
public static class StringStaticFunctions
{
    /// <summary>
    ///     Formats a message string with placeholders using the specified format string and arguments.
    /// </summary>
    /// <param name="formatString">The format string with numbered placeholders.</param>
    /// <param name="args">An array of objects to replace the placeholders in the format string.</param>
    /// <returns>The formatted message string.</returns>
    public static string FormatMessage(string formatString, params object[] args)
    {
        // Using numbered placeholders for clarity
        for (int i = 0; i < args.Length; i++)
        {
            // Replace placeholders like {0}, {1}, etc.
            formatString = formatString.Replace($"{{{i}}}", args[i].ToString());
        }

        return formatString;
    }

    /// <summary>
    ///     Normalizes and trims the input string by converting it to lowercase and removing leading and trailing white spaces.
    /// </summary>
    /// <param name="input">The string to normalize and trim.</param>
    /// <returns>A normalized and trimmed string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input string is null or white space.</exception>
    public static string NormalizeAndTrim(this string input)
    {
        // Check input
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException(nameof(input), ErrorMessages.InputCannotBeNullWhiteSpaceMsg);

        // Return result
        return input.ToLower().Trim();
    }
}