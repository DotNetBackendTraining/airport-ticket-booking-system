using AirportTicketBookingSystem.Application.Interfaces;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Infrastructure.Converter;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public class AirportConverterShould : ConverterTestBase<Airport>
{
    protected override ICsvEntityConverter<Airport> GetConverter() => new AirportConverter();

    private const char NameQuote = CsvConstants.AirportNameQuoteChar;

    public static IEnumerable<object[]> ValidEntityCsvTestData()
    {
        yield return
        [
            new Airport("0", "Los Angeles International Airport", "USA"),
            $"0,{NameQuote}Los Angeles International Airport{NameQuote},USA"
        ];
        yield return
        [
            new Airport("1", "Beijing Capital International Airport", "China"),
            $"1,{NameQuote}Beijing Capital International Airport{NameQuote},China"
        ];
    }

    public static IEnumerable<object[]> InvalidCsvTestData()
    {
        yield return [""];
        yield return ["0,USA"];
        yield return [$"Australia,{NameQuote}China{NameQuote}"];
        yield return ["1,Australia,China"];
    }

    [Theory]
    [MemberData(nameof(ValidEntityCsvTestData))]
    public override void CsvToEntity_ConvertValidCsvToEntity(Airport expectedEntity, string validCsvLine)
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
    public override void EntityToCsv_ConvertEntityToCsv(Airport entity, string expectedCsvLine)
    {
        base.EntityToCsv_ConvertEntityToCsv(entity, expectedCsvLine);
    }
}