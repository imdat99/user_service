using Account.Application.Common;
using MediatR;

namespace Account.Application.Commands;

public class UpdateUserCommand : IRequest<Result<Guid>>
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}