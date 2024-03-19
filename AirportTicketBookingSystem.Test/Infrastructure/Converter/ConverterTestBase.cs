using AirportTicketBookingSystem.Domain.Interfaces;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public abstract class ConverterTestBase<TEntity> where TEntity : IEntity
{
    protected abstract ICsvEntityConverter<TEntity> GetConverter();

    public virtual void CsvToEntity_ConvertValidCsvToEntity(TEntity expectedEntity, string validCsvLine)
    {
        var converter = GetConverter();
        var entity = converter.CsvToEntity(validCsvLine);
        Assert.NotNull(entity);
        Assert.Equal(entity, expectedEntity);
    }

    public virtual void CsvToEntity_ThrowFormatExceptionIfInvalidCsv(string invalidCsvLine)
    {
        var converter = GetConverter();
        Assert.Throws<FormatException>(() => converter.CsvToEntity(invalidCsvLine));
    }

    public virtual void EntityToCsv_ConvertEntityToCsv(TEntity entity, string expectedCsvLine)
    {
        var converter = GetConverter();
        var csvLine = converter.EntityToCsv(entity);
        Assert.Equal(csvLine, expectedCsvLine);
    }
}