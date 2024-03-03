using System.Text;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Contract;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class FlightConverter : ICsvEntityConverter<Flight>
{
    public Flight CsvToEntity(string csvLine)
    {
        var parts = Parser.SplitToMinLengthOrThrow(csvLine, 4);
        var id = Parser.ParseOrThrowInt(parts[0]);
        var departureDate = Parser.ParseOrThrowDate(parts[1], CsvConstants.FlightDepartureDateTimeFormat);
        var departureAirportId = parts[2];
        var arrivalAirportId = parts[3];

        Dictionary<FlightClass, decimal> classPrices = new();
        for (var i = 4; i < parts.Length; i++)
        {
            var (flightClass, price) = FlightClassParser.ParseFullFlightClassAndPriceOrThrow(parts[i]);
            classPrices[flightClass] = price;
        }

        return Flight.Create(id, departureDate, departureAirportId, arrivalAirportId, classPrices);
    }

    public string EntityToCsv(Flight entity)
    {
        StringBuilder sb = new();
        sb.Append(entity.Id);
        sb.Append(',');
        sb.Append(entity.DepartureDate.ToString(CsvConstants.FlightDepartureDateTimeFormat));
        sb.Append(',');
        sb.Append(entity.DepartureAirportId);
        sb.Append(',');
        sb.Append(entity.ArrivalAirportId);

        foreach (var kvp in entity.ClassPrices)
        {
            sb.Append(',');
            sb.Append(CsvConstants.FlightClassPrefix);
            sb.Append($"{kvp.Key}{CsvConstants.FlightClassPriceSplitterChar}{kvp.Value}");
        }

        return sb.ToString();
    }
}