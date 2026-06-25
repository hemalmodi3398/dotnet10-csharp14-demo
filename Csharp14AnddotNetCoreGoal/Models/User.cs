namespace Csharp14AnddotNetCoreGoal.Models;

/// <summary>
/// User model demonstrating C# 14 features including field keyword in auto-properties
/// </summary>
public class User
{
    public required int Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    // Demonstrates field keyword in an auto-property accessor.
    public required string Email
    {
        get;
        set => field = value.Trim().ToLowerInvariant();
    }
    
    public string? PhoneNumber { get; set; }
    
    public required string City { get; set; }
    
    public required string Country { get; set; }
    
    public required string Department { get; set; }
    
    public required string JobTitle { get; set; }
    
    public int Age { get; set; }

    public DateOnly? Birthdate { get; set; }
    
    // Caches computed full name to keep reads fast and simple.
    private string? _fullNameCache;
    public string FullName
    {
        get
        {
            // Use null-conditional assignment (??=) - C# 8 feature, still relevant
            return _fullNameCache ??= $"{FirstName} {LastName}";
        }
    }
    
    private bool _isActive = true;
    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }
}
