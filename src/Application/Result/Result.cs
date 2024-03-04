namespace AirportTicketBookingSystem.Application.Result;

/// <summary>
/// Represents the base result of an operation, containing the success status and a message.
/// </summary>
/// <param name="Success">Indicates whether the operation was successful.</param>
/// <param name="Message">Provides a message related to the operation outcome.</param>
public abstract record Result(bool Success, string Message);