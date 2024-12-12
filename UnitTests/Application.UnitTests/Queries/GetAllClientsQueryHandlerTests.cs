using Application.Interfaces;
using Application.Queries;
using AutoFixture;
using Domain.Entities;
using Moq;
using Shouldly;

[TestFixture]
public class GetAllClientsQueryHandlerTests
{
    private Mock<IClientRepository> _mockRepository;
    private GetAllClientsQueryHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IClientRepository>();
        _handler = new GetAllClientsQueryHandler(_mockRepository.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Test]
    public async Task Handle_ShouldReturnAllClients()
    {
        // Arrange
        var clients = _fixture.CreateMany<Client>(3).ToList();
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(clients);

        // Act
        var result = await _handler.Handle(new GetAllClientsQuery(), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(clients);
        _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }
}