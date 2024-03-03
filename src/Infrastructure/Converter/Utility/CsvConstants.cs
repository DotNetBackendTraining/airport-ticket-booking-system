namespace AirportTicketBookingSystem.Infrastructure.Converter.Utility;

public static class CsvConstants
{
    // Standard date and time format for flight departure times in CSV files
    public const string FlightDepartureDateTimeFormat = "yyyy-MM-dd HH:mm";

    // All airport names should be quoted with it in CSV files
    public const char AirportNameQuoteChar = '`';

    // All flight class fields should be combined with it in CSV files
    public const char FlightClassFieldsDelimiter = ';';

    // All flight class-price pairs should be combined with it in CSV files
    public const char FlightClassPriceDelimiter = ':';
}