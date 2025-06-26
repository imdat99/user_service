namespace Account.Application.Interfaces;

public interface IUserProfileService
{
    Task CreateDefaultProfileAsync(Guid userId, CancellationToken cancellationToken);
}