using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity>(
    string filepath,
    ICsvEntityConverter<TEntity> csvEntityConverter
) : IFileService<TEntity>
{
    private string Filepath { get; } = filepath;
    private ICsvEntityConverter<TEntity> Converter { get; } = csvEntityConverter;

    public IEnumerable<TEntity> ReadAll()
    {
        return File.ReadLines(Filepath)
            .Skip(1) // Skip header
            .Select(line => Converter.CsvToEntity(line));
    }

    public async Task WriteAllAsync(IEnumerable<TEntity> entities)
    {
        var csvLines = entities.Select(entity => Converter.EntityToCsv(entity));
        await File.WriteAllLinesAsync(Filepath, csvLines);
    }
}