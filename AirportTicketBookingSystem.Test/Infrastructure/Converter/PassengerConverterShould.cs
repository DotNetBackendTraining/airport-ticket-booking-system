using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Infrastructure.Converter;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public class PassengerConverterShould : ConverterTestBase<Passenger>
{
    protected override ICsvEntityConverter<Passenger> GetConverter() => new PassengerConverter();

    public static IEnumerable<object[]> ValidEntityCsvTestData()
    {
        yield return [new Passenger(0), "0"];
        yield return [new Passenger(1), "1"];
        yield return [new Passenger(-1), "-1"];
        yield return [new Passenger(123), "123"];
    }

    public static IEnumerable<object[]> InvalidCsvTestData()
    {
        yield return [""];
        yield return ["test"];
        yield return ["1,1"];
    }

    [Theory]
    [MemberData(nameof(ValidEntityCsvTestData))]
    public override void CsvToEntity_ConvertValidCsvToEntity(Passenger expectedEntity, string validCsvLine)
    {
        base.CsvToEntity_ConvertValidCsvToEntity(expectedEntity, validCsvLine);
    }

    [Theory]
    [MemberData(nameof(InvalidCsvTestData))]
    public override void CsvToEntity_ThrowFormatExceptionIfInvalidCsv(string invalidCsvLine)
    {
        base.CsvToEntity_ThrowFormatExceptionIfInvalidCsv(invalidCsvLine);
    }

    [Theory]
    [MemberData(nameof(ValidEntityCsvTestData))]
    public override void EntityToCsv_ConvertEntityToCsv(Passenger entity, string expectedCsvLine)
    {
        base.EntityToCsv_ConvertEntityToCsv(entity, expectedCsvLine);
    }
}