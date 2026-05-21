namespace Csharp14AnddotNetCoreGoal.Services;

using System.Text.Json;
using Csharp14AnddotNetCoreGoal.Extensions;
using Csharp14AnddotNetCoreGoal.Models;

/// <summary>
/// Service for managing user data from JSON file
/// Demonstrates modern C# patterns including null-conditional assignment
/// </summary>
public class UserService
{
    private List<User>? _users;
    private readonly string _jsonFilePath;
    private readonly ILogger<UserService> _logger;

    public UserService(ILogger<UserService> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _jsonFilePath = Path.Combine(env.ContentRootPath, "Data", "users.json");
    }

    /// <summary>
    /// Gets all users from the JSON file
    /// Demonstrates null-conditional assignment (??=)
    /// </summary>
    public async Task<List<User>> GetAllUsersAsync()
    {
        // Use null-conditional assignment to lazily load users
        // This is C# 8+ feature but still relevant for modern C#
        _users ??= await LoadUsersFromFileAsync();
        
        return _users;
    }

    /// <summary>
    /// Gets a user by ID
    /// </summary>
    public async Task<User?> GetUserByIdAsync(int id)
    {
        var users = await GetAllUsersAsync();
        return users.FirstOrDefault(u => u.Id == id);
    }

    /// <summary>
    /// Creates a new user and persists it to the JSON file.
    /// </summary>
    public async Task<User> CreateUserAsync(CreateUserRequest request)
    {
        var users = await GetAllUsersAsync();
        var nextId = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;

        var user = new User
        {
            Id = nextId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            City = request.City,
            Country = request.Country,
            Department = request.Department,
            JobTitle = request.JobTitle,
            Age = request.Age,
            Birthdate = request.Birthdate,
            IsActive = true
        };

        users.Add(user);
        await SaveUsersToFileAsync(users);

        return user;
    }

    /// <summary>
    /// Searches users by a search term
    /// </summary>
    public async Task<List<User>> SearchUsersAsync(string searchTerm)
    {
        var users = await GetAllUsersAsync();
        
        if (string.IsNullOrWhiteSpace(searchTerm))
            return users;

        // Using extension methods from UserExtensions
        return users.Where(u => u.MatchesSearch(searchTerm)).ToList();
    }

    /// <summary>
    /// Gets users by department
    /// </summary>
    public async Task<List<User>> GetUsersByDepartmentAsync(string department)
    {
        var users = await GetAllUsersAsync();
        
        // Using extension method from UserCollectionExtensions
        return users.FromDepartment(department).ToList();
    }

    /// <summary>
    /// Gets users by city
    /// </summary>
    public async Task<List<User>> GetUsersByCityAsync(string city)
    {
        var users = await GetAllUsersAsync();
        
        // Using extension method from UserCollectionExtensions
        return users.FromCity(city).ToList();
    }

    /// <summary>
    /// Gets users in a specific age range
    /// </summary>
    public async Task<List<User>> GetUsersByAgeRangeAsync(int minAge, int maxAge)
    {
        var users = await GetAllUsersAsync();
        
        // Using extension method from UserCollectionExtensions
        return users.InAgeRange(minAge, maxAge).ToList();
    }

    /// <summary>
    /// Gets statistics about users
    /// </summary>
    public async Task<UserStatistics> GetUserStatisticsAsync()
    {
        var users = await GetAllUsersAsync();
        
        return new UserStatistics
        {
            TotalUsers = users.Count,
            AverageAge = users.Average(u => u.Age),
            DepartmentCounts = users.GroupBy(u => u.Department)
                                    .ToDictionary(g => g.Key, g => g.Count()),
            CityCounts = users.GroupBy(u => u.City)
                             .OrderByDescending(g => g.Count())
                             .Take(10)
                             .ToDictionary(g => g.Key, g => g.Count()),
            SeniorEmployees = users.Count(u => u.IsSenior())
        };
    }

    /// <summary>
    /// Loads users from the JSON file
    /// </summary>
    private async Task<List<User>> LoadUsersFromFileAsync()
    {
        try
        {
            _logger.LogInformation("Loading users from {FilePath}", _jsonFilePath);
            
            if (!File.Exists(_jsonFilePath))
            {
                _logger.LogWarning("Users file not found at {FilePath}", _jsonFilePath);
                return new List<User>();
            }

            var jsonContent = await File.ReadAllTextAsync(_jsonFilePath);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var users = JsonSerializer.Deserialize<List<User>>(jsonContent, options);
            
            _logger.LogInformation("Successfully loaded {Count} users", users?.Count ?? 0);
            
            return users ?? new List<User>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading users from file");
            throw;
        }
    }

    private async Task SaveUsersToFileAsync(List<User> users)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonContent = JsonSerializer.Serialize(users, options);
        await File.WriteAllTextAsync(_jsonFilePath, jsonContent);
    }

    /// <summary>
    /// Reloads users from the file (clears cache)
    /// </summary>
    public async Task ReloadUsersAsync()
    {
        _users = null;
        await GetAllUsersAsync();
    }
}

/// <summary>
/// Statistics about users
/// </summary>
public record UserStatistics
{
    public required int TotalUsers { get; init; }
    public required double AverageAge { get; init; }
    public required Dictionary<string, int> DepartmentCounts { get; init; }
    public required Dictionary<string, int> CityCounts { get; init; }
    public required int SeniorEmployees { get; init; }
}
