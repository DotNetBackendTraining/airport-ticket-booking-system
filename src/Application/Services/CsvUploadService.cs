using AirportTicketBookingSystem.Application.Contract;
using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain.Contract;

namespace AirportTicketBookingSystem.Application.Services;

public class CsvUploadService<TEntity> : IUploadService<TEntity> where TEntity : IEntity
{
    private readonly ICsvEntityConverter<TEntity> _csvEntityConverter;
    
    public CsvUploadService(ICsvEntityConverter<TEntity> csvEntityConverter)
    {
        _csvEntityConverter = csvEntityConverter;
    }

    public IEnumerable<OperationResult<TEntity>> BatchUpload(string filepath)
    {
        // try to use exiting libraries to read common file types
        // you can use CsvHelper to read csv files
        // https://joshclose.github.io/CsvHelper/
        // it will save your time and help you to avoid common mistakes
        foreach (var line in File.ReadLines(filepath).Skip(1))
        {
            var success = true;
            var message = "Entity validation completed successfully";
            TEntity? entity = default;
            try
            {
                entity = _csvEntityConverter.CsvToEntity(line);
            }
            catch (Exception e)
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