namespace Account.Application.Commands;

public class CreateUserCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserProfileService _profileService;

    public CreateUserCommandHandler(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IUserProfileService profileService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _profileService = profileService;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user with same email already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
        {
            return Result<string>.Failure("A user with this email already exists.");
        }

        // Check if username is provided and if it's unique
        if (!string.IsNullOrWhiteSpace(request.Username) && 
            await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken))
        {
            return Result<string>.Failure("A user with this username already exists.");
        }

        // Hash password
        string passwordHash = _passwordHasher.HashPassword(request.Password);

        // Create user
        var user = new User(
            request.Email,
            passwordHash,
            request.FirstName,
            request.LastName
        );

        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            user.SetUsername(request.Username);
        }

        if (!string.IsNullOrWhiteSpace(request.Phone))
        {
            user.UpdateContactInfo(request.Phone, null);
        }

        if (request.DateOfBirth.HasValue)
        {
            user.UpdatePersonalInfo(request.FirstName, request.LastName, request.DateOfBirth);
        }

        // Save user
        bool success = await _userRepository.AddAsync(user, cancellationToken);
        if (!success)
        {
            return Result<string>.Failure("Failed to create user.");
        }

        // Create default profile
        await _profileService.CreateDefaultProfileAsync(user.Id, cancellationToken);

        return Result<string>.Success(user.Id);
    }
}
