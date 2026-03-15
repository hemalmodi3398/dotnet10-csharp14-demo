# C# 14 and .NET 9 User Fetching System Demo

This project demonstrates modern C# and .NET 9 features through a simple user management API that reads data from a JSON file.

## Features Demonstrated

### C# Features

1. **Required Properties** (`required` keyword)
   - See `User.cs` - all essential properties use `required` modifier
   - Ensures properties are initialized during object creation

2. **Extension Methods** (Modern extension patterns)
   - See `UserExtensions.cs` - demonstrates various extension methods
   - `GetDisplayName()`, `MatchesSearch()`, `IsSenior()`, etc.
   - Collection extensions like `FromDepartment()`, `InAgeRange()`

3. **Null-Conditional Assignment** (`??=` operator)
   - See `UserService.cs` - `_users ??= await LoadUsersFromFileAsync()`
   - Lazy loading pattern with null-coalescing assignment

4. **Record Types**
   - See `UserStatistics` in `UserService.cs`
   - Immutable data transfer objects with init-only properties

5. **Pattern Matching**
   - See `Program.cs` - `user is not null` pattern
   - Modern null-checking patterns

6. **Expression-Bodied Members**
   - See `UserExtensions.cs` - `IsSenior()` method uses `=>`

### .NET 9 Features

1. **Minimal APIs**
   - All user endpoints in `Program.cs` use Minimal API pattern
   - Clean, concise endpoint definitions
   - Built-in dependency injection

2. **Top-Level Statements**
   - `Program.cs` uses top-level statements (no Main method)

3. **Global Using Directives**
   - Implicit usings enabled in project file

## Project Structure

```
Csharp14AnddotNetCoreGoal/
??? Data/
?   ??? users.json              # 100 sample users
??? Models/
?   ??? User.cs                 # User model with modern C# features
??? Extensions/
?   ??? UserExtensions.cs       # Extension methods for User operations
??? Services/
?   ??? UserService.cs          # Service for managing user data
??? Program.cs                  # Minimal API endpoints
```

## API Endpoints

### 1. Get All Users
```
GET /api/users
```
Returns all 100 users from the JSON file.

### 2. Get User by ID
```
GET /api/users/{id}
```
Returns a specific user by ID.

**Example:** `GET /api/users/1`

### 3. Search Users
```
GET /api/users/search?term={searchTerm}
```
Searches users by name, email, city, or department.

**Example:** `GET /api/users/search?term=john`

### 4. Get Users by Department
```
GET /api/users/department/{department}
```
Returns all users in a specific department.

**Example:** `GET /api/users/department/Engineering`

Available departments:
- Engineering
- Product
- Design
- Sales
- Marketing
- HR
- Finance
- Operations

### 5. Get Users by City
```
GET /api/users/city/{city}
```
Returns all users in a specific city.

**Example:** `GET /api/users/city/New York`

### 6. Get Users by Age Range
```
GET /api/users/age?min={minAge}&max={maxAge}
```
Returns users within the specified age range.

**Example:** `GET /api/users/age?min=25&max=35`

### 7. Get User Statistics
```
GET /api/users/statistics
```
Returns statistical information about all users:
- Total user count
- Average age
- Department distribution
- Top 10 cities
- Senior employee count (age >= 40)

### 8. Get User Summary
```
GET /api/users/{id}/summary
```
Returns a formatted summary of a user using extension methods.

**Example:** `GET /api/users/1/summary`

Returns:
- Display name with job title
- Full summary text
- Masked email for privacy
- Senior status

### 9. Reload Users
```
POST /api/users/reload
```
Clears the cache and reloads users from the JSON file.

## Running the Project

1. **Build the project:**
   ```bash
   dotnet build
   ```

2. **Run the project:**
   ```bash
   dotnet run
   ```

3. **Access Swagger UI:**
   - Open your browser and navigate to: **https://localhost:7075**
   - Swagger UI will display all available endpoints with interactive testing
   - You can test all endpoints directly from the browser!

4. **Alternative: Direct API access:**
   - API endpoints are available at `https://localhost:7075/api/...`
   - Example: `https://localhost:7075/api/users`

## Testing Examples

### Using cURL

```bash
# Get all users
curl https://localhost:7000/api/users

# Get user by ID
curl https://localhost:7000/api/users/1

# Search for users
curl https://localhost:7000/api/users/search?term=engineer

# Get users in Engineering department
curl https://localhost:7000/api/users/department/Engineering

# Get users in age range 25-35
curl https://localhost:7000/api/users/age?min=25&max=35

# Get statistics
curl https://localhost:7000/api/users/statistics

# Get user summary
curl https://localhost:7000/api/users/1/summary
```

### Using Browser (Swagger UI - Recommended!)

The **easiest way** to test the API is using Swagger UI:

1. Run the project: `dotnet run`
2. Open browser to: **https://localhost:7075**
3. You'll see a beautiful interactive API documentation
4. Click on any endpoint ? "Try it out" ? Enter parameters ? "Execute"
5. See the results instantly!

### Using Browser (Direct URLs)

Simply paste these URLs in your browser:
- `https://localhost:7075/api/users`
- `https://localhost:7075/api/users/statistics`
- `https://localhost:7075/api/users/search?term=engineer`

But really, just use **Swagger UI** at https://localhost:7075 - it's much nicer! ??

## Code Highlights

### Extension Methods Usage
```csharp
// From Program.cs - using extension methods
var summary = user.GetSummary();
var isSenior = user.IsSenior();
var displayName = user.GetDisplayName();
```

### Null-Conditional Assignment
```csharp
// From UserService.cs - lazy loading with ??=
_users ??= await LoadUsersFromFileAsync();
```

### Pattern Matching
```csharp
// From Program.cs - modern null checking
return user is not null 
    ? Results.Ok(user) 
    : Results.NotFound(new { Message = $"User with ID {id} not found" });
```

### Minimal API Pattern
```csharp
// Clean, concise endpoint definitions
app.MapGet("/api/users/{id:int}", async (int id, UserService userService) =>
{
    var user = await userService.GetUserByIdAsync(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
})
.WithName("GetUserById")
.WithOpenApi();
```

## Notes

- This is a **learning/demo project** - not production-ready
- Data is loaded from JSON file (no database)
- Users are cached in memory for performance
- All data is read-only (no create/update/delete operations)
- Perfect for demonstrating modern C# and .NET features locally

## Technologies Used

- **.NET 9.0**
- **C# 12** (latest features available in .NET 9)
- **ASP.NET Core Minimal APIs**
- **System.Text.Json** for JSON serialization
- **OpenAPI/Swagger** support

---

**Note:** While this project is labeled for C# 14 and .NET 10, it currently uses C# 12 and .NET 9 as those are the latest released versions. The code structure and patterns demonstrated here will be compatible with future versions and showcase the latest best practices.
