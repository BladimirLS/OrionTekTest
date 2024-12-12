using Application.Commands;
using Application.Interfaces;
using Moq;

[TestFixture]
public class DeleteClientCommandHandlerTests
{
    private Mock<IClientRepository> _mockRepository;
    private DeleteClientCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IClientRepository>();
        _handler = new DeleteClientCommandHandler(_mockRepository.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldDeleteClient()
    {
        // Arrange
        var command = new DeleteClientCommand(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockRepository.Verify(r => r.DeleteAsync(command.Id), Times.Once);
    }
}