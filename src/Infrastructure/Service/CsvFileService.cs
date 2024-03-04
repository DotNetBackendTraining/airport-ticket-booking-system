using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity> : IFileService<TEntity>
    where TEntity : IEntity
{
    private string Filepath { get; }
    private ICsvEntityConverter<TEntity> Converter { get; }
    private IEnumerable<string> Header { get; }

    public CsvFileService(string filepath,
        ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        Filepath = filepath;
        Converter = csvEntityConverter;
        Header = File.ReadLines(Filepath).Take(1);
    }

    public IEnumerable<TEntity> ReadAll()
    {
        return File.ReadLines(Filepath)
            .Skip(1) // Skip header
            .Select(line => Converter.CsvToEntity(line));
    }

    private readonly SemaphoreSlim _writeLock = new(1, 1);

    public async Task WriteAllAsync(IEnumerable<TEntity> entities)
    {
        await _writeLock.WaitAsync();
        try
        {
            var csvLines = entities.Select(entity => Converter.EntityToCsv(entity));
            await File.WriteAllLinesAsync(Filepath, Header.Concat(csvLines));
        }
        finally
        {
            _writeLock.Release();
        }
    }

    public async Task AppendAllAsync(IEnumerable<TEntity> entities)
    {
        await _writeLock.WaitAsync();
        try
        {
            var csvLines = entities.Select(entity => Converter.EntityToCsv(entity));
            await File.AppendAllLinesAsync(Filepath, csvLines);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}