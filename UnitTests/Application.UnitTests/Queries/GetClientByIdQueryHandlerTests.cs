using Application.Interfaces;
using Application.Queries;
using AutoFixture;
using Domain.Entities;
using Moq;
using Shouldly;

[TestFixture]
public class GetClientByIdQueryHandlerTests
{
    private Mock<IClientRepository> _mockRepository;
    private GetClientByIdQueryHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IClientRepository>();
        _handler = new GetClientByIdQueryHandler(_mockRepository.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Test]
    public async Task Handle_ClientExists_ShouldReturnClient()
    {
        // Arrange
        var client = _fixture.Create<Client>();
        _mockRepository.Setup(repo => repo.GetByIdAsync(client.Id)).ReturnsAsync(client);

        // Act
        var result = await _handler.Handle(new GetClientByIdQuery(client.Id), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        result.ShouldBe(client);
        _mockRepository.Verify(repo => repo.GetByIdAsync(client.Id), Times.Once);
    }

    [Test]
    public async Task Handle_ClientDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Client)null);

        // Act
        var result = await _handler.Handle(new GetClientByIdQuery(1), CancellationToken.None);

        // Assert
        result.ShouldBeNull();
        _mockRepository.Verify(repo => repo.GetByIdAsync(1), Times.Once);
    }
}