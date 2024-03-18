using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Service;

public class CsvFileService<TEntity>(
    string filepath,
    ICsvEntityConverter<TEntity> csvEntityConverter
) : IFileService<TEntity> where TEntity : IEntity
{
    private string Filepath { get; } = filepath;
    private ICsvEntityConverter<TEntity> Converter { get; } = csvEntityConverter;
    private string? Header { get; set; }

    public IEnumerable<TEntity> ReadAll()
    {
        var entities = new List<TEntity>();
        using var stream = new FileStream(Filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new StreamReader(stream);

        Header = reader.ReadLine();
        while (reader.ReadLine() is { } line)
        {
            try
            {
                var entity = Converter.CsvToEntity(line);
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
            await using var stream = new FileStream(Filepath, FileMode.Create, FileAccess.Write, FileShare.Read);
            await using var writer = new StreamWriter(stream);
            foreach (var csvLine in Enumerable
                         .Repeat(Header, 1)
                         .Concat(entities.Select(Converter.EntityToCsv)))
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
            await using var stream = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read);
            await using var writer = new StreamWriter(stream);
            foreach (var csvLine in entities.Select(Converter.EntityToCsv))
                await writer.WriteLineAsync(csvLine);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}