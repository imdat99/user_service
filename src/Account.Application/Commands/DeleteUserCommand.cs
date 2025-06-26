using Account.Application.Common;
using MediatR;

namespace Account.Application.Commands;

public class DeleteUserCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
}
