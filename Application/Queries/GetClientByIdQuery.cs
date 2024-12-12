using Domain.Entities;
using MediatR;

namespace Application.Queries;

public record GetClientByIdQuery(int Id) : IRequest<Client>;