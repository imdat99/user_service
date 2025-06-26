using Account.Application.Interfaces;
using Account.Domain.Entities;
using Account.Domain.Repositories;

namespace Account.Application.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateDefaultProfileAsync(Guid userId, CancellationToken cancellationToken)
        {
            // Logic to create a default profile for the user
            // This could involve creating a profile entity in the database
            // or initializing some default settings for the user.
            
            // Example:
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if (user != null)
            {
                // Initialize default profile settings here
                // e.g., user.Profile = new UserProfile { ... };
                user.UserProfile ??= new UserProfile(userId);
                await _userRepository.UpdateAsync(user, cancellationToken);
            }
        }
    }
}
