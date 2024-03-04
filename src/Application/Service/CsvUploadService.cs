using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Application.Service;

public class CsvUploadService<TEntity>(
    ICsvEntityConverter<TEntity> csvEntityConverter
) : IUploadService<TEntity> where TEntity : IEntity
{
    private ICsvEntityConverter<TEntity> CsvEntityConverter { get; } = csvEntityConverter;

    public IEnumerable<OperationResult<TEntity>> BatchUpload(string filepath)
    {
        foreach (var line in File.ReadLines(filepath).Skip(1))
        {
            var success = true;
            var message = "Entity validation completed successfully";
            TEntity? entity = default;
            try
            {
                entity = CsvEntityConverter.CsvToEntity(line);
            }
            catch (SystemException e)
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