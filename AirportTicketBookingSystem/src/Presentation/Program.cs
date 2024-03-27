using AirportTicketBookingSystem.Presentation;
using AirportTicketBookingSystem.Presentation.Controller;
using AirportTicketBookingSystem.Presentation.Utility;
using dotenv.net;

DotEnv.Load();

if (PromptHelper.PromptYesNo("Do You Want to Enter Manager Mode (y/n):  "))
{
    var provider = DependencyInjector.CreateManagerServiceProvider();
    new ManagerController(provider).Start();
}
else
{
    var provider = DependencyInjector.CreateClientServiceProvider();
    new ClientController(provider).Start();
}