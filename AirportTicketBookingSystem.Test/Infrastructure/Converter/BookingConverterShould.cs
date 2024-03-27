using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Infrastructure.Converter;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public class BookingConverterShould : ConverterTestBase<Booking>
{
    protected override ICsvEntityConverter<Booking> GetConverter() => new BookingConverter();

    public static IEnumerable<object[]> ValidEntityCsvTestData()
    {
        yield return [new Booking(9, 0, FlightClass.FirstClass), "9,0,FirstClass"];
        yield return [new Booking(1, 1, FlightClass.Business), "1,1,Business"];
    }

    public static IEnumerable<object[]> InvalidCsvTestData()
    {
        yield return [""];
        yield return ["9,0,First Class"];
        yield return ["9,FirstClass,3"];
        yield return ["9,0"];
    }

    [Theory]
    [MemberData(nameof(ValidEntityCsvTestData))]
    public override void CsvToEntity_ConvertValidCsvToEntity(Booking expectedEntity, string validCsvLine)
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
    public override void EntityToCsv_ConvertEntityToCsv(Booking entity, string expectedCsvLine)
    {
        base.EntityToCsv_ConvertEntityToCsv(entity, expectedCsvLine);
    }
}