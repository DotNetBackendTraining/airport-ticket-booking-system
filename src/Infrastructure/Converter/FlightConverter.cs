using System.Text;
using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;
using AirportTicketBookingSystem.Infrastructure.Interfaces;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class FlightConverter : ICsvEntityConverter<Flight>
{
    private static Dictionary<FlightClass, decimal> ParseFlightClassPricePairsOrThrow(string flightClassStr)
    {
        var parts = flightClassStr.Split(CsvConstants.FlightClassFieldsDelimiter);
        Dictionary<FlightClass, decimal> res = new();
        foreach (var part in parts)
        {
            var subs = part.SplitToLengthOrThrow(2, CsvConstants.FlightClassPriceDelimiter);
            var flightClass = subs[0].ParseFlightClassOrThrow();
            var price = subs[1].ParseOrThrowDecimal();
            res[flightClass] = price;
        }

        return res;
    }

    public Flight CsvToEntity(string csvLine)
    {
        var parts = csvLine.SplitToLengthOrThrow(5);
        var id = parts[0].ParseOrThrowInt();
        var departureDate = parts[1].ParseOrThrowDate(CsvConstants.FlightDepartureDateTimeFormat);
        var departureAirportId = parts[2];
        var arrivalAirportId = parts[3];
        var classPrices = ParseFlightClassPricePairsOrThrow(parts[4]);
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
        sb.Append(',');

        var fields = entity.ClassPrices.Select(
            kvp => $"{kvp.Key}{CsvConstants.FlightClassPriceDelimiter}{kvp.Value}");
        sb.Append(string.Join(CsvConstants.FlightClassFieldsDelimiter, fields));

        return sb.ToString();
    }
}