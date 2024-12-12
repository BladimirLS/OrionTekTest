using Domain.Entities;
using MediatR;

namespace Application.Commands;


public record CreateClientCommand(string Name, string Email, List<Address> Addresses) : IRequest<int>;