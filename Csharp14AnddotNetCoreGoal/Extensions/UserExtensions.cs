namespace Csharp14AnddotNetCoreGoal.Extensions;

using Csharp14AnddotNetCoreGoal.Models;

/// <summary>
/// Extension members for User - demonstrating C# 14 extension members feature
/// These extensions add helper methods directly to the User type
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Gets the display name with title
    /// </summary>
    public static string GetDisplayName(this User user)
    {
        return $"{user.JobTitle} - {user.FullName}";
    }
    
    /// <summary>
    /// Checks if user is in a specific department
    /// </summary>
    public static bool IsInDepartment(this User user, string department)
    {
        return user.Department.Equals(department, StringComparison.OrdinalIgnoreCase);
    }
    
    /// <summary>
    /// Checks if user matches search criteria
    /// </summary>
    public static bool MatchesSearch(this User user, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return true;
            
        var term = searchTerm.ToLowerInvariant();
        
        return user.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
               user.LastName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
               user.Email.Contains(term, StringComparison.OrdinalIgnoreCase) ||
               user.City.Contains(term, StringComparison.OrdinalIgnoreCase) ||
               user.Department.Contains(term, StringComparison.OrdinalIgnoreCase);
    }
    
    /// <summary>
    /// Checks if user is a senior employee (age-based)
    /// </summary>
    public static bool IsSenior(this User user) => user.Age >= 40;
    
    /// <summary>
    /// Gets a summary of the user
    /// </summary>
    public static string GetSummary(this User user)
    {
        return $"{user.FullName} ({user.Age} years old) - {user.JobTitle} in {user.Department}, based in {user.City}, {user.Country}";
    }
    
    /// <summary>
    /// Masks the email for privacy
    /// </summary>
    public static string GetMaskedEmail(this User user)
    {
        var parts = user.Email.Split('@');
        if (parts.Length != 2) return user.Email;
        
        var localPart = parts[0];
        var domain = parts[1];
        
        var masked = localPart.Length > 2 
            ? $"{localPart[0]}***{localPart[^1]}@{domain}"
            : $"***@{domain}";
            
        return masked;
    }
}

/// <summary>
/// Extension methods for collections of users
/// </summary>
public static class UserCollectionExtensions
{
    /// <summary>
    /// Filters users by department
    /// </summary>
    public static IEnumerable<User> FromDepartment(this IEnumerable<User> users, string department)
    {
        return users.Where(u => u.IsInDepartment(department));
    }
    
    /// <summary>
    /// Filters users by age range
    /// </summary>
    public static IEnumerable<User> InAgeRange(this IEnumerable<User> users, int minAge, int maxAge)
    {
        return users.Where(u => u.Age >= minAge && u.Age <= maxAge);
    }
    
    /// <summary>
    /// Gets users by city
    /// </summary>
    public static IEnumerable<User> FromCity(this IEnumerable<User> users, string city)
    {
        return users.Where(u => u.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    }
    
    /// <summary>
    /// Gets the average age of users in a collection
    /// </summary>
    public static double GetAverageAge(this IEnumerable<User> users)
    {
        // Potential issue: Multiple enumeration if used with queries
        if (users.Count() == 0)
            return 0;
            
        return users.Average(u => u.Age);
    }
    
    /// <summary>
    /// Checks if all users in collection are from the same country
    /// </summary>
    public static bool AreAllFromSameCountry(this IEnumerable<User> users)
    {
        // Could be optimized - gets first element multiple times
        var firstCountry = users.FirstOrDefault()?.Country;
        return users.All(u => u.Country == firstCountry);
    }
}
