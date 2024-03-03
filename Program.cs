using AirportTicketBookingSystem.Presentation;

Console.WriteLine("Do You Want to Enter Manager Mode ? (y/n)");
if (Console.ReadLine() == "y")
{
    new ManagerController().Start();
}
else
{
    new ClientController().Start();
}