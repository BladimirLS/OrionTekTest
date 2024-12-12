using Application.Commands;
using Application.Interfaces;
using AutoFixture;
using Domain.Entities;
using Moq;
using Shouldly;

[TestFixture]
public class CreateClientCommandHandlerTests
{
    private Mock<IClientRepository> _mockRepository;
    private CreateClientCommandHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IClientRepository>();
        _handler = new CreateClientCommandHandler(_mockRepository.Object);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldReturnNewClientId()
    {
        // Arrange
        var command = _fixture.Create<CreateClientCommand>();
        var client = new Client { Id = 1, Name = command.Name, Email = command.Email, Addresses = command.Addresses };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Client>())).Callback<Client>(c => c.Id = client.Id);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(client.Id);
        _mockRepository.Verify(r => r.AddAsync(It.Is<Client>(c => c.Name == command.Name && c.Email == command.Email)), Times.Once);
    }
}