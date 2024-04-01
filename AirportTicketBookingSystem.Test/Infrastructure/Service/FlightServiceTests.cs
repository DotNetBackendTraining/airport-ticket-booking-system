using AirportTicketBookingSystem.Domain;
using AirportTicketBookingSystem.Domain.Criteria.Search;
using AirportTicketBookingSystem.Domain.Interfaces.Repository;
using AirportTicketBookingSystem.Domain.Interfaces.Service;
using AirportTicketBookingSystem.Infrastructure.Service;
using AirportTicketBookingSystem.Test.Common;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Xunit;

namespace AirportTicketBookingSystem.Test.Infrastructure.Service;

public class FlightServiceTests
{
    [Theory, AutoMoqData]
    public async Task AddAsync_ShouldCallRepository(
        Flight entity,
        [Frozen] Mock<IFlightRepository> repositoryMock,
        FlightService service)
    {
        await service.AddAsync(entity);
        repositoryMock.Verify(r => r.AddAsync(entity), Times.Once);
    }

    [Theory, AutoMoqData]
    public void GetById_ShouldCallRepository(
        Flight entity,
        [Frozen] Mock<IFlightRepository> repositoryMock,
        FlightService service)
    {
        repositoryMock
            .Setup(r => r.GetById(entity.Id))
            .Returns(entity);

        var returned = service.GetById(entity.Id);

        returned.Should().Be(entity);
        repositoryMock.Verify(r => r.GetById(entity.Id), Times.Once);
    }

    [Theory, AutoMoqData]
    public void Search_ShouldUseFilterOnAllEntities(
        FlightSearchCriteria criteria,
        List<Flight> allEntities,
        List<Flight> filteredEntities,
        [Frozen] Mock<IFlightRepository> repositoryMock,
        [Frozen] Mock<IFilteringService<Flight, FlightSearchCriteria>> filteringServiceMock,
        FlightService service)
    {
        repositoryMock
            .Setup(r => r.GetAll())
            .Returns(allEntities);
        filteringServiceMock
            .Setup(s => s.Filter(allEntities, criteria))
            .Returns(filteredEntities);

        var returned = service.Search(criteria);

        returned.Should().BeSameAs(filteredEntities);
        repositoryMock.Verify(r => r.GetAll(), Times.Once);
        filteringServiceMock.Verify(s => s.Filter(allEntities, criteria), Times.Once);
    }
}