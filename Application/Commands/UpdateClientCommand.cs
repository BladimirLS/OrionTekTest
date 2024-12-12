using Domain.Entities;
using MediatR;

namespace Application.Commands;
public record UpdateClientCommand(int Id, string Name, string Email, List<Address> Addresses) : IRequest<Unit>;