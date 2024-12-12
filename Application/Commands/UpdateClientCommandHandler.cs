using Application.Interfaces;
using MediatR;

namespace Application.Commands;



public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Unit>
{
    private readonly IClientRepository _repository;

    public UpdateClientCommandHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _repository.GetByIdAsync(request.Id);
        if (client == null) throw new KeyNotFoundException($"Client with Id {request.Id} not found.");

        client.Name = request.Name;
        client.Email = request.Email;
        client.Addresses = request.Addresses;

        await _repository.UpdateAsync(client);

        return Unit.Value;
    }
}

