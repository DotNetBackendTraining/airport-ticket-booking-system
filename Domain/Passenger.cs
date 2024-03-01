namespace AirportTicketBookingSystem.Domain;

public class Passenger(int id)
{
    public int Id { get; } = id;
    public List<Booking> Bookings { get; set; } = [];

    public override bool Equals(object? obj) =>
        obj is Passenger other && Id == other.Id;

    public override int GetHashCode() => Id.GetHashCode();
}