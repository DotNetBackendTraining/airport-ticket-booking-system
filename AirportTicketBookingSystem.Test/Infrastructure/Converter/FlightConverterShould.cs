using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Converter;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Converter;

public class FlightConverterShould : ConverterTestBase<Flight>
{
    protected override ICsvEntityConverter<Flight> GetConverter() => new FlightConverter();

    public static IEnumerable<object[]> ValidEntityCsvTestData()
    {
        yield return
        [
            new Flight(0, new DateTime(2024, 3, 5, 10, 0, 0), "1", "3", new Dictionary<FlightClass, decimal>
            {
                { FlightClass.Economy, 200.5M },
                { FlightClass.Business, 500M },
                { FlightClass.FirstClass, 800M }
            }),
            "0,2024-03-05 10:00,1,3,Economy:200.50;Business:500.00;FirstClass:800.00"
        ];
    }

    public static IEnumerable<object[]> InvalidCsvTestData()
    {
        yield return [""];
        yield return ["1,2024-03-06-15:30,2,3,Economy:150.00;Business:450.00;FirstClass:700.00"];
        yield return ["1,2024-03-06 15:30,2 3,Economy:150.00;Business:450.00;FirstClass:700.00"];
        yield return ["1,2024-03-06 15:30,2,3,Economy:150.00:Business:450.00:FirstClass:700.00"];
        yield return ["1,2024-03-06 15:30,2,3,Economy;Business;FirstClass"];
        yield return ["2024-03-06 15:30,2,3,Economy:150.00:Business:450.00:FirstClass:700.00"];
    }

    [Theory]
    [MemberData(nameof(ValidEntityCsvTestData))]
    public override void CsvToEntity_ConvertValidCsvToEntity(Flight expectedEntity, string validCsvLine)
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
    public override void EntityToCsv_ConvertEntityToCsv(Flight entity, string expectedCsvLine)
    {
        base.EntityToCsv_ConvertEntityToCsv(entity, expectedCsvLine);
    }
}