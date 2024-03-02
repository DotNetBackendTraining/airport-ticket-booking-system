using System.Globalization;

namespace AirportTicketBookingSystem.Infrastructure.Converter.Utility;

/// <summary>
/// Provides utility methods for parsing different data types from strings with strict format requirements.
/// Throws exceptions if parsing fails to match the expected formats.
/// </summary>
public static class Parser
{
    /// <summary>
    /// Parses a date string into a DateTime object based on the specified format.
    /// </summary>
    /// <param name="dateStr">The date string to parse.</param>
    /// <param name="format">The expected date format.</param>
    /// <returns>A DateTime object representing the parsed date.</returns>
    /// <exception cref="FormatException">Thrown if the date string does not match the specified format.</exception>
    public static DateTime ParseOrThrowDate(string dateStr, string format)
    {
        if (!DateTime.TryParseExact(dateStr, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var res))
            throw new FormatException($"Date '{dateStr}' does not match format '{format}'.");
        return res;
    }

    /// <summary>
    /// Parses a string into an integer. Throws an exception if parsing fails.
    /// </summary>
    /// <param name="intStr">The string to parse as an integer.</param>
    /// <returns>The parsed integer value.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as an integer.</exception>
    public static int ParseOrThrowInt(string intStr)
    {
        if (!int.TryParse(intStr, out var intValue))
            throw new FormatException($"Unable to parse '{intStr}' as integer.");
        return intValue;
    }

    /// <summary>
    /// Parses a string into a decimal. Throws an exception if parsing fails.
    /// </summary>
    /// <param name="decimalStr">The string to parse as a decimal.</param>
    /// <returns>The parsed decimal value.</returns>
    /// <exception cref="FormatException">Thrown if the string cannot be parsed as a decimal.</exception>
    public static decimal ParseOrThrowDecimal(string decimalStr)
    {
        if (!decimal.TryParse(decimalStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
            throw new FormatException($"Unable to parse '{decimalStr}' as decimal.");
        return decimalValue;
    }

    /// <summary>
    /// Removes leading and trailing quotation marks from a string and throws an exception if the string is not properly quoted.
    /// </summary>
    /// <param name="str">The string to unquote.</param>
    /// <param name="quoteChar">The quotation character to expect at the beginning and end of the string.</param>
    /// <returns>The unquoted string.</returns>
    /// <exception cref="FormatException">Thrown if the string is not enclosed in the expected quotation marks.</exception>
    public static string UnquoteOrThrowString(string str, char quoteChar)
    {
        if (!str.StartsWith(quoteChar) || !str.EndsWith(quoteChar))
            throw new FormatException($"The string '{str}' is expected to be enclosed in {quoteChar} quotes.");
        return str.Substring(1, str.Length - 2);
    }

    /// <summary>
    /// Splits a string into the specified number of parts using the provided splitter character and throws an exception if the string cannot be split into the expected number of parts.
    /// </summary>
    /// <param name="str">The string to split.</param>
    /// <param name="numberOfParts">The expected number of parts after splitting.</param>
    /// <param name="splitter">The character used to split the string. Defaults to a comma.</param>
    /// <returns>An array of strings, each representing a part of the original string.</returns>
    /// <exception cref="FormatException">Thrown if the string does not split into the expected number of parts.</exception>
    public static string[] SplitToLengthOrThrow(string str, int numberOfParts, char splitter = ',')
    {
        var parts = str.Split(splitter);
        if (parts.Length != numberOfParts)
            throw new FormatException($"Unable to split '{str}' into {numberOfParts} parts separated by '{splitter}'.");
        return parts;
    }

    /// <summary>
    /// Splits a string into at least the specified number of parts using the provided splitter character
    /// and throws an exception if the string cannot be split into at least the expected number of parts.
    /// </summary>
    /// <param name="str">The string to split.</param>
    /// <param name="minNumberOfParts">The minimum expected number of parts after splitting.</param>
    /// <param name="splitter">The character used to split the string. Defaults to a comma.</param>
    /// <returns>An array of strings, each representing a part of the original string.</returns>
    /// <exception cref="FormatException">Thrown if the string does not split into at least the expected number of parts.</exception>
    public static string[] SplitToMinLengthOrThrow(string str, int minNumberOfParts, char splitter = ',')
    {
        var parts = str.Split(splitter);
        if (parts.Length < minNumberOfParts)
            throw new FormatException(
                $"Unable to split '{str}' into at least {minNumberOfParts} parts separated by '{splitter}'.");
        return parts;
    }
}