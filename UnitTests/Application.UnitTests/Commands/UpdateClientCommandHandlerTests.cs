using Application.Commands;
using Application.Interfaces;
using AutoFixture;
using Domain.Entities;
using Moq;
using Shouldly;

[TestFixture]
public class UpdateClientCommandHandlerTests
{
    private Mock<IClientRepository> _mockRepository;
    private UpdateClientCommandHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IClientRepository>();
        _handler = new UpdateClientCommandHandler(_mockRepository.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldUpdateClient()
    {
        // Arrange
        var command = _fixture.Create<UpdateClientCommand>();
        var existingClient = _fixture.Build<Client>().With(c => c.Id, command.Id).Create();

        _mockRepository.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(existingClient);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.UpdateAsync(It.Is<Client>(c => c.Id == command.Id && c.Name == command.Name)), Times.Once);
    }

    [Test]
    public void Handle_ClientNotFound_ShouldThrowException()
    {
        // Arrange
        var command = _fixture.Create<UpdateClientCommand>();
        _mockRepository.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Client)null);

        // Act & Assert
        Should.ThrowAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}