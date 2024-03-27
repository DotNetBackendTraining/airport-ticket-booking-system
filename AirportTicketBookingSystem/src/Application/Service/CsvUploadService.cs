using AirportTicketBookingSystem.Application.Interfaces.Service;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain.Interfaces;

namespace AirportTicketBookingSystem.Application.Service;

public class CsvUploadService<TEntity> : IUploadService<TEntity> where TEntity : IEntity
{
    private readonly ICsvEntityConverter<TEntity> _csvEntityConverter;
    public CsvUploadService(ICsvEntityConverter<TEntity> csvEntityConverter) => _csvEntityConverter = csvEntityConverter;

    public IEnumerable<OperationResult<TEntity>> BatchUpload(string filepath)
    {
        foreach (var line in File.ReadLines(filepath).Skip(1))
        {
            var success = true;
            var message = "Entity validation completed successfully";
            TEntity? entity = default;
            try
            {
                entity = _csvEntityConverter.CsvToEntity(line);
            }
            catch (FormatException e)
            {
                success = false;
                message = "Entity validation failed:  " + e.Message;
            }

            yield return new OperationResult<TEntity>(
                Success: success,
                Message: message,
                Item: entity);
        }
    }
}