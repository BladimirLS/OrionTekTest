using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Commands;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
{
    private readonly IClientRepository _repository;

    public CreateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client { Name = request.Name, Email = request.Email, Addresses = request.Addresses };
        await _repository.AddAsync(client);
        return client.Id;
    }
}