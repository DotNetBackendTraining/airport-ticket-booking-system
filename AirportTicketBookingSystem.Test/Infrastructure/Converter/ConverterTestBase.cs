using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain.Interfaces;
using FluentAssertions;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public abstract class ConverterTestBase<TEntity> where TEntity : IEntity
{
    protected abstract ICsvEntityConverter<TEntity> GetConverter();

    public virtual void CsvToEntity_ConvertValidCsvToEntity(TEntity expectedEntity, string validCsvLine)
    {
        var converter = GetConverter();
        var entity = converter.CsvToEntity(validCsvLine);
        entity.Should().NotBeNull().And.Be(expectedEntity);
    }

    public virtual void CsvToEntity_ThrowFormatExceptionIfInvalidCsv(string invalidCsvLine)
    {
        var converter = GetConverter();
        var converting = () => converter.CsvToEntity(invalidCsvLine);
        converting.Should().Throw<FormatException>();
    }

    public virtual void EntityToCsv_ConvertEntityToCsv(TEntity entity, string expectedCsvLine)
    {
        var converter = GetConverter();
        var csvLine = converter.EntityToCsv(entity);
        csvLine.Should().Be(expectedCsvLine);
    }
}