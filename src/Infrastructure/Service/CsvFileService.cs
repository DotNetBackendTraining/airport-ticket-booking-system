using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity>
    : IFileService<TEntity>
    where TEntity : IEntity
{
    private readonly string _filepath;
    private readonly ICsvEntityConverter<TEntity> _converter;
    private string? Header { get; set; }
    
    public CsvFileService(string filepath, ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        _filepath = filepath;
        _converter = csvEntityConverter;
    }

    public IEnumerable<TEntity> ReadAll()
    {
        var entities = new List<TEntity>();
        using var stream = new FileStream(_filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream);

        Header = reader.ReadLine();
        while (reader.ReadLine() is { } line)
        {
            try
            {
                var entity = _converter.CsvToEntity(line);
                entities.Add(entity);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return entities;
    }

    private readonly SemaphoreSlim _writeLock = new(1, 1);

    public async Task WriteAllAsync(IEnumerable<TEntity> entities)
    {
        await _writeLock.WaitAsync();
        try
        {
            await using var stream = new FileStream(_filepath, FileMode.Create, FileAccess.Write, FileShare.Read);
            await using var writer = new StreamWriter(stream);
            foreach (var csvLine in Enumerable
                         .Repeat(Header, 1)
                         .Concat(entities.Select(_converter.EntityToCsv)))
                await writer.WriteLineAsync(csvLine);
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
            await using var stream = new FileStream(_filepath, FileMode.Append, FileAccess.Write, FileShare.Read);
            await using var writer = new StreamWriter(stream);
            foreach (var csvLine in entities.Select(_converter.EntityToCsv))
                await writer.WriteLineAsync(csvLine);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}