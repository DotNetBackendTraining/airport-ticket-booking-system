using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces;
using AirportTicketBookingSystem.Infrastructure.Converter.Utility;

namespace AirportTicketBookingSystem.Infrastructure.Converter;

public class AirportConverter : ICsvEntityConverter<Airport>
{
    public Airport CsvToEntity(string csvLine)
    {
        var parts = csvLine.SplitToLengthOrThrow(3);
        var id = parts[0];
        var name = parts[1].UnquoteOrThrow(CsvConstants.AirportNameQuoteChar);
        var country = parts[2];
        return Airport.Create(id, name, country);
    }

    public string EntityToCsv(Airport airport)
    {
        const char q = CsvConstants.AirportNameQuoteChar;
        return $"{airport.Id},{q}{airport.Name}{q},{airport.Country}";
    }
}