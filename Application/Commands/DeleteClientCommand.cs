using MediatR;

namespace Application.Commands;

public record DeleteClientCommand(int Id) : IRequest<Unit>;