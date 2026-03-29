# C# 14 and .NET 10 Demo (Simple User API)

This project is a simple demo API for **C# 14** and **.NET 10** features.
It keeps the original structure and style intentionally straightforward for learning and presentation.

## Important Note About Build on Visual Studio 2022

Your setup may not build this project if:

- `.NET 10 SDK` is not installed, or
- the current Visual Studio version does not support `.NET 10` tooling.

That is expected for now. The code and project settings are upgraded for .NET 10/C# 14 demonstration.

## Project Structure

```text
Csharp14AnddotNetCoreGoal/
├── Controllers/
│   └── WeatherForecastController.cs
├── Data/
│   └── users.json
├── Extensions/
│   └── UserExtensions.cs
├── Models/
│   ├── ResponseDtos.cs
│   └── User.cs
├── Properties/
│   └── launchSettings.json
├── Services/
│   └── UserService.cs
├── appsettings.Development.json
├── appsettings.json
├── Csharp14AnddotNetCoreGoal.csproj
├── Csharp14AnddotNetCoreGoal.http
├── Program.cs
└── WeatherForecast.cs
```

## What This Demo Covers

### C# 14 Features (and modern C# patterns)

1. **Extension members (extension blocks)**
   - File: `Extensions/UserExtensions.cs`
   - Includes:
     - `extension(User user)` with extension properties and methods
     - `extension(IEnumerable<User> users)` for collection helpers
   - Example members:
     - `DisplayName` (extension property)
     - `Summary` (extension property)
     - `MatchesSearch()`, `IsSenior()`, `FromDepartment()`, etc.

2. **`field` keyword in property accessor**
   - File: `Models/User.cs`
   - `Email` property normalizes incoming value using `field` in setter.

3. **Required members**
   - File: `Models/User.cs`, `Services/UserService.cs`, `Models/ResponseDtos.cs`
   - `required` is used on critical properties to enforce initialization.

4. **Record types / positional records**
   - File: `Models/ResponseDtos.cs`, `Services/UserService.cs`
   - Demonstrates immutable-style DTO modeling.

5. **Pattern matching**
   - File: `Program.cs`
   - Null checks with `is null` / `is not null` style branches.

6. **Null-coalescing assignment**
   - File: `Services/UserService.cs`
   - `_users ??= await LoadUsersFromFileAsync();` for lazy cache load.

### .NET 10 / ASP.NET Core Features

1. **Minimal APIs**
   - File: `Program.cs`
   - Endpoint mapping with dependency injection and typed response metadata.

2. **Top-level statements**
   - File: `Program.cs`
   - No explicit `Main` method required.

3. **OpenAPI/Swagger integration**
   - File: `Program.cs`
   - API docs and Swagger UI for testing endpoints.

4. **Dependency injection and logging**
   - Files: `Program.cs`, `Services/UserService.cs`
   - Service registration and constructor injection.

## API Endpoints

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by id
- `GET /api/users/search?term={term}` - Search by name/email/city/department
- `GET /api/users/department/{department}` - Filter by department
- `GET /api/users/city/{city}` - Filter by city
- `GET /api/users/age?min={min}&max={max}` - Filter by age range
- `GET /api/users/statistics` - Aggregate statistics
- `GET /api/users/{id}/summary` - Uses extension members
- `POST /api/users/reload` - Reload data from file

## Quick Run Commands

```bash
dotnet restore
dotnet build
dotnet run
```

Then open Swagger UI at your local app URL (usually root path in Development).

## Tech Settings Used

- Target framework: `net10.0`
- Language version: `preview` (for C# 14 features)
- Preview features enabled in project file

---

This is a **demo/learning** project, intentionally simple and readable rather than production-oriented.
