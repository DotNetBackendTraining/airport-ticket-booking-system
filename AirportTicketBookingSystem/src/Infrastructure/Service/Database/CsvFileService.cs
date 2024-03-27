using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service.Database;

public class CsvFileService<TEntity> : IFileService<TEntity> where TEntity : IEntity
{
    private readonly string _filepath;
    private readonly ICsvEntityConverter<TEntity> _converter;

    public CsvFileService(
        string filepath,
        ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        _filepath = filepath;
        _converter = csvEntityConverter;
    }

    private string? _header;

    public IEnumerable<TEntity> ReadAll()
    {
        var entities = new List<TEntity>();
        using var stream = new FileStream(_filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream);

        _header = reader.ReadLine();
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
                         .Repeat(_header, 1)
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