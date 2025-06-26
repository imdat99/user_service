using Account.Application.Common;
using Account.Application.DTOs;
using MediatR;

namespace Account.Application.Queries;

public class GetAllUsersQuery : IRequest<Result<IEnumerable<UserDto>>>
{
}
