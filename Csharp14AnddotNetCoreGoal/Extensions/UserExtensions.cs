namespace Csharp14AnddotNetCoreGoal.Extensions;

using Csharp14AnddotNetCoreGoal.Models;

/// <summary>
/// Extension members for User - demonstrating C# 14 extension members feature
/// These extensions add helper methods directly to the User type
/// </summary>
public static class UserExtensions
{
    extension(User user)
    {
        /// <summary>
        /// Gets the display name with title as an extension property.
        /// </summary>
        public string DisplayName => $"{user.JobTitle} - {user.FullName}";

        /// <summary>
        /// Gets the user summary as an extension property.
        /// </summary>
        public string Summary => $"{user.FullName} ({user.Age} years old) - {user.JobTitle} in {user.Department}, based in {user.City}, {user.Country}";

        /// <summary>
        /// Checks if user is in a specific department.
        /// </summary>
        public bool IsInDepartment(string department) =>
            user.Department.Equals(department, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Checks if user matches search criteria.
        /// </summary>
        public bool MatchesSearch(string searchTerm)
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
        /// Checks if user is a senior employee (age-based).
        /// </summary>
        public bool IsSenior() => user.Age >= 40;

        /// <summary>
        /// Masks the email for privacy.
        /// </summary>
        public string GetMaskedEmail()
        {
            var parts = user.Email.Split('@');
            if (parts.Length != 2) return user.Email;

            var localPart = parts[0];
            var domain = parts[1];

            return localPart.Length > 2
                ? $"{localPart[0]}***{localPart[^1]}@{domain}"
                : $"***@{domain}";
        }
    }
}

/// <summary>
/// Extension methods for collections of users
/// </summary>
public static class UserCollectionExtensions
{
    extension(IEnumerable<User> users)
    {
        /// <summary>
        /// Filters users by department.
        /// </summary>
        public IEnumerable<User> FromDepartment(string department) =>
            users.Where(u => u.IsInDepartment(department));

        /// <summary>
        /// Filters users by age range.
        /// </summary>
        public IEnumerable<User> InAgeRange(int minAge, int maxAge) =>
            users.Where(u => u.Age >= minAge && u.Age <= maxAge);

        /// <summary>
        /// Gets users by city.
        /// </summary>
        public IEnumerable<User> FromCity(string city) =>
            users.Where(u => u.City.Equals(city, StringComparison.OrdinalIgnoreCase));
    }
}
