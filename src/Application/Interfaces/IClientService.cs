using AirportTicketBookingSystem.Application.Result;
using AirportTicketBookingSystem.Domain;

namespace AirportTicketBookingSystem.Application.Interfaces;

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
    /// Adds a new booking to the system.
    /// </summary>
    /// <param name="booking">The booking to add.</param>
    /// <returns>An OperationResult corresponding to the addition operation of the booking.</returns>
    public OperationResult<Booking> AddBooking(Booking booking);

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
    /// <param name="cancelledBooking">The cancelled booking information containing the changes.</param>
    /// <returns>An OperationResult indicating whether the cancellation was successful, including a message.</returns>
    public OperationResult<Booking> CancelBooking(Booking cancelledBooking);

    /// <summary>
    /// Checks if the passenger is registered within the system.
    /// </summary>
    /// <param name="passengerId">The ID of the passenger to check.</param>
    /// <returns>Whether the passenger is found in the system.</returns>
    public bool IsRegisteredPassenger(int passengerId);
}