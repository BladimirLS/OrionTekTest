using Domain.Entities;
using MediatR;

namespace Application.Queries;


public record GetAllClientsQuery() : IRequest<IEnumerable<Client>>;