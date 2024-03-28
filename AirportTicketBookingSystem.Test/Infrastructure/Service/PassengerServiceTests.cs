using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Infrastructure.Service;
using AirportTicketBookingSystem.Test.Common;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service;

public class PassengerServiceTests
{
    [Theory, AutoMoqData]
    public void Add_ShouldCallRepository(
        Passenger entity,
        [Frozen] Mock<IPassengerRepository> repositoryMock,
        PassengerService service)
    {
        service.Add(entity);
        repositoryMock.Verify(r => r.Add(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GetById_ShouldCallRepository(
        Passenger entity,
        [Frozen] Mock<IPassengerRepository> repositoryMock,
        PassengerService service)
    {
        repositoryMock
            .Setup(r => r.GetById(entity.Id))
            .Returns(entity);

        var returned = service.GetById(entity.Id);

        returned.Should().Be(entity);
        repositoryMock.Verify(r => r.GetById(entity.Id), Times.Once);
    }
}