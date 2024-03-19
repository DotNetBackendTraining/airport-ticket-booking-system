namespace AirportTicketBookingSystem.Domain.Common;

/// <summary>
/// Represents errors that occur during database operations.
/// </summary>
public class DatabaseOperationException : DatabaseException
{
    public DatabaseOperationException()
    {
    }

    public DatabaseOperationException(string message) : base(message)
    {
    }

    public DatabaseOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}