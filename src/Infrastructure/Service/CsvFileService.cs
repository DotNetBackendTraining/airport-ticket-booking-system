using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity> : IFileService<TEntity>
    where TEntity : IEntity
{
    private string Filepath { get; }
    private ICsvEntityConverter<TEntity> Converter { get; }
    private string Header { get; }

    public CsvFileService(string filepath,
        ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        Filepath = filepath;
        Converter = csvEntityConverter;
        Header = File.ReadLines(Filepath).Take(1).ToList()[0];
    }

    public IEnumerable<TEntity> ReadAll()
    {
        return File.ReadLines(Filepath)
            .Skip(1) // Skip header
            .Select(line => Converter.CsvToEntity(line));
    }

    public async Task WriteAllAsync(IEnumerable<TEntity> entities)
    {
        var csvLines = entities.Select(entity => Converter.EntityToCsv(entity));
        await File.WriteAllLinesAsync(Filepath, Enumerable.Repeat(Header, 1).Concat(csvLines));
    }
}