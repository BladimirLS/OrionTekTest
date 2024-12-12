using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands;
using Application.Queries;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.Entities;
using MediatR;
using Moq;
using NUnit.Framework;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using OrionTekTest.Controllers;

[TestFixture]
public class ClientsControllerTests
{
    private IFixture _fixture;
    private Mock<IMediator> _mockMediator;
    private ClientsController _controller;

    [SetUp]
    public void SetUp()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockMediator = _fixture.Freeze<Mock<IMediator>>();
        _controller = new ClientsController(_mockMediator.Object);
    }
    [Test]
    public async Task GetAll_ShouldReturnOkWithClients()
    {
        // Arrange
        var fixture = new Fixture();

        // Prevent circular references
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var clients = fixture.CreateMany<Client>(5).ToList();
    
        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetAllClientsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(clients);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.ShouldNotBeNull();
        okResult.StatusCode.ShouldBe(200);
        var returnedClients = okResult.Value as IEnumerable<Client>;
        returnedClients.ShouldBe(clients);
    }

    [Test]
    public async Task GetById_ClientExists_ShouldReturnOkWithClient()
    {
        // Arrange
        var fixture = new Fixture();

        // Prevent circular references
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var client = fixture.Create<Client>();
        _mockMediator
            .Setup(m => m.Send(It.IsAny<GetClientByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(client);

        // Act
        var result = await _controller.GetById(client.Id);

        // Assert
        var okResult = result.ShouldBeOfType<OkObjectResult>();
        okResult.Value.ShouldBe(client);
    }


    [Test]
    public async Task GetById_ClientDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        _mockMediator.Setup(m => m.Send(It.IsAny<GetClientByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Client)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        result.ShouldBeOfType<NotFoundResult>();
    }
    [Test]
    public async Task Create_ValidCommand_ShouldReturnCreatedWithId()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var command = fixture.Create<CreateClientCommand>();

        _mockMediator.Setup(m => m.Send(It.IsAny<CreateClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.Create(command);

        // Assert
        var createdResult = result as CreatedAtActionResult;
        createdResult.ShouldNotBeNull();
        createdResult.StatusCode.ShouldBe(201);
        createdResult.Value.ShouldBe(command);
    }

    [Test]
    public async Task Update_InvalidCommand_ShouldReturnBadRequest()
    {
        // Arrange
        var fixture = new Fixture();

        // Prevent circular references
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var command = fixture.Build<UpdateClientCommand>()
            .With(c => c.Id, 1)
            .Create();

        _mockMediator
            .Setup(m => m.Send(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Update(2, command);

        // Assert
        result.ShouldBeOfType<BadRequestResult>();
    }


    [Test]
    public async Task Update_IdMismatch_ShouldReturnBadRequest()
    {
        // Arrange
        var fixture = new Fixture();
        
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var command = fixture.Build<UpdateClientCommand>()
            .With(c => c.Id, 1) 
            .Create();

        _mockMediator
            .Setup(m => m.Send(It.IsAny<UpdateClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Update(2, command); 

        // Assert
        result.ShouldBeOfType<BadRequestResult>();
    }



    [Test]
    public async Task Delete_ValidId_ShouldReturnNoContent()
    {
        // Arrange
        var clientId = _fixture.Create<int>();
        _mockMediator.Setup(m => m.Send(It.IsAny<DeleteClientCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.Delete(clientId);

        // Assert
        result.ShouldBeOfType<NoContentResult>();
    }

}