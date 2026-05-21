namespace Csharp14AnddotNetCoreGoal.Models;

/// <summary>
/// Response DTOs demonstrating modern C# record types
/// Records provide value-based equality and immutability
/// </summary>

/// <summary>
/// User summary response using positional record
/// </summary>
public record UserSummaryResponse(
    int Id,
    string DisplayName,
    string Summary,
    string MaskedEmail,
    bool IsSenior
);

/// <summary>
/// Search results response
/// </summary>
public record SearchResultsResponse
{
    public required string SearchTerm { get; init; }
    public required int Count { get; init; }
    public required List<User> Users { get; init; }
}

/// <summary>
/// Department users response
/// </summary>
public record DepartmentUsersResponse
{
    public required string Department { get; init; }
    public required int Count { get; init; }
    public required List<User> Users { get; init; }
}

/// <summary>
/// Error response
/// </summary>
public record ErrorResponse(string Message, int StatusCode);

/// <summary>
/// Success response
/// </summary>
public record SuccessResponse(string Message);

/// <summary>
/// Create user request body
/// </summary>
public record CreateUserRequest
{
    public const int MinAllowedAge = 0;
    public const int MaxAllowedAge = 150;

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public string? PhoneNumber { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public required string Department { get; init; }
    public required string JobTitle { get; init; }
    public int Age { get; init; }
    public DateOnly? Birthdate { get; init; }
}
