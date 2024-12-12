using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

[TestFixture]
public class ClientRepositoryTests
{
    private ApplicationDbContext _context;
    private ClientRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
            .Options;

        _context = new ApplicationDbContext(options);
        _repository = new ClientRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllClients()
    {
        // Arrange
        var clients = new List<Client>
        {
            new Client { Name = "John Doe", Email = "john@example.com" },
            new Client { Name = "Jane Smith", Email = "jane@example.com" }
        };

        await _context.Clients.AddRangeAsync(clients);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Count().ShouldBe(2);
    }

    [Test]
    public async Task GetByIdAsync_ClientExists_ShouldReturnClient()
    {
        // Arrange
        var client = new Client { Name = "John Doe", Email = "john@example.com" };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(client.Id);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(client.Name);
        result.Email.ShouldBe(client.Email);
    }

    [Test]
    public async Task GetByIdAsync_ClientDoesNotExist_ShouldReturnNull()
    {
        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public async Task AddAsync_ShouldAddClientToDatabase()
    {
        // Arrange
        var client = new Client { Name = "John Doe", Email = "john@example.com" };

        // Act
        await _repository.AddAsync(client);
        var result = await _context.Clients.FirstOrDefaultAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(client.Name);
        result.Email.ShouldBe(client.Email);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateExistingClient()
    {
        // Arrange
        var client = new Client { Name = "John Doe", Email = "john@example.com" };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act
        client.Name = "John Updated";
        client.Email = "updated@example.com";
        await _repository.UpdateAsync(client);

        var result = await _context.Clients.FirstOrDefaultAsync();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe("John Updated");
        result.Email.ShouldBe("updated@example.com");
    }

    [Test]
    public async Task DeleteAsync_ClientExists_ShouldRemoveClientFromDatabase()
    {
        // Arrange
        var client = new Client { Name = "John Doe", Email = "john@example.com" };
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(client.Id);

        var result = await _context.Clients.FirstOrDefaultAsync();

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public async Task DeleteAsync_ClientDoesNotExist_ShouldNotThrowException()
    {
        // Act & Assert
        Should.NotThrowAsync(async () => await _repository.DeleteAsync(999));
    }
}
