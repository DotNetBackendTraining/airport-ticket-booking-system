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
    /// <returns>A collection of bookings for the specified passenger.</returns>
    public IEnumerable<Booking> GetAllBookings(int passengerId);

    /// <summary>
    /// Updates an existing flight booking for a passenger.
    /// ID fields are used to find which booking and for which flight & passenger is it.
    /// The rest of fields are updated according to the new values.
    /// </summary>
    /// <param name="updatedBooking">The updated booking information containing the changes.</param>
    /// <returns>A boolean indicating whether the update was successful.</returns>
    public bool UpdateBooking(Booking updatedBooking);

    /// <summary>
    /// Cancels a flight booking for the passenger.
    /// </summary>
    /// <param name="flightId">The ID of the flight.</param>
    /// <param name="passengerId">The ID of the passenger.</param>
    /// <returns>A boolean indicating whether the cancellation was successful.</returns>
    public bool CancelBooking(int flightId, int passengerId);
}