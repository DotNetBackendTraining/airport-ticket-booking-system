using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Contract;

/// <summary>
/// Defines the service operations available to clients
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Retrieves all bookings for a given passenger.
    /// </summary>
    /// <param name="passengerId">The ID of the passenger whose bookings to retrieve.</param>
    /// <returns>A SearchResult containing a collection of bookings for the specified passenger.</returns>
    public SearchResult<Booking> GetAllBookings(int passengerId);

    /// <summary>
    /// Updates an existing flight booking for a passenger.
    /// The booking to be updated is identified by ID fields within the updatedBooking parameter.
    /// </summary>
    /// <param name="updatedBooking">The updated booking information containing the changes.</param>
    /// <returns>An OperationResult indicating whether the update was successful, including a message.</returns>
    public OperationResult<Booking> UpdateBooking(Booking updatedBooking);

    /// <summary>
    /// Cancels a flight booking for the passenger.
    /// </summary>
    /// <param name="flightId">The ID of the flight to cancel.</param>
    /// <param name="passengerId">The ID of the passenger canceling the booking.</param>
    /// <returns>An OperationResult indicating whether the cancellation was successful, including a message.</returns>
    public OperationResult<Booking> CancelBooking(int flightId, int passengerId);

    /// <summary>
    /// Checks if the passenger is registered within the system.
    /// </summary>
    /// <param name="passengerId">The ID of the passenger to check.</param>
    /// <returns>Whether the passenger is found in the system.</returns>
    public bool AuthenticatePassenger(int passengerId);
}