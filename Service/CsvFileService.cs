namespace AirportTicketBookingSystem.Service;

/// <summary>
/// Provides static methods for reading from and writing to CSV files.
/// </summary>
public static class CsvFileService
{
    /// <summary>
    /// Reads data from a CSV file and converts each line into an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="filepath">The path to the CSV file to be read.</param>
    /// <param name="converter">A function that converts a single CSV line into an instance of <typeparamref name="T"/>.</param>
    /// <param name="skipHeader">Specifies whether to skip the first line of the file, often used to ignore header rows. Defaults to false.</param>
    /// <typeparam name="T">The type of object each line of the CSV file will be converted into.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> where each element is an instance of <typeparamref name="T"/> representing a line from the CSV file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    public static IEnumerable<T> ReadCsvFile<T>(
        string filepath, Func<string, T> converter, bool skipHeader = false)
    {
        if (!File.Exists(filepath))
            throw new FileNotFoundException($"The file {filepath} was not found.");

        using var reader = new StreamReader(filepath);
        while (reader.ReadLine() is { } line)
        {
            if (skipHeader)
            {
                skipHeader = false;
                continue;
            }

            var data = converter(line);
            yield return data;
        }
    }

    /// <summary>
    /// Writes a collection of data items to a CSV file, converting each item to a CSV line using the provided converter function.
    /// </summary>
    /// <param name="filepath">The path to the CSV file to be written.</param>
    /// <param name="dataItems">An <see cref="IEnumerable{T}"/> of data items to be written to the file.</param>
    /// <param name="converter">A function that converts an instance of <typeparamref name="T"/> into a CSV line.</param>
    /// <typeparam name="T">The type of object that will be converted into a line for the CSV file.</typeparam>
    public static void WriteCsvFile<T>(
        string filepath, IEnumerable<T> dataItems, Func<T, string> converter)
    {
        using var writer = new StreamWriter(filepath);
        foreach (var data in dataItems)
        {
            var line = converter(data);
            writer.WriteLine(line);
        }
    }
}