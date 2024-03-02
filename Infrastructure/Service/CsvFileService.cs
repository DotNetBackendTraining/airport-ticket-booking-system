using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<T>(
    string filepath,
    ICsvEntityConverter<T> csvEntityConverter
) : IFileService<T>
{
    private string Filepath { get; } = filepath;
    private ICsvEntityConverter<T> Converter { get; } = csvEntityConverter;

    public IEnumerable<T> ReadAll()
    {
        if (!File.Exists(Filepath))
            throw new FileNotFoundException($"The file {Filepath} was not found.");

        return File.ReadLines(Filepath)
            .Skip(1) // header
            .Select(line => Converter.CsvToEntity(line));
    }

    public async Task AppendAsync(IEnumerable<T> entities)
    {
        var csvLines = entities.Select(entity => Converter.EntityToCsv(entity));
        await File.AppendAllLinesAsync(Filepath, csvLines);
    }
}