using Csharp14AnddotNetCoreGoal.Extensions;
using Csharp14AnddotNetCoreGoal.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management API - C# 14 & .NET 10 Demo",
        Version = "v1",
        Description = "A simple user management API demonstrating modern C# 14 and .NET 10 features including:\n\n" +
                      "- Extension members (extension blocks)\n" +
                      "- Null-conditional assignment\n" +
                      "- Pattern matching\n" +
                      "- Record types\n" +
                      "- Minimal APIs\n" +
                      "- Required properties",
        Contact = new OpenApiContact
        {
            Name = "User Management API",
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI at root
        options.DocumentTitle = "User Management API";
        options.DefaultModelsExpandDepth(2);
        options.DefaultModelExpandDepth(2);
    });
}

app.UseHttpsRedirection();

// ========================================
// Minimal API Endpoints for User Management
// Demonstrating .NET 10 Minimal APIs
// ========================================

// GET /api/users - Get all users
app.MapGet("/api/users", async (UserService userService) =>
{
    var users = await userService.GetAllUsersAsync();
    return Results.Ok(users);
})
.WithName("GetAllUsers")
.WithDescription("Retrieves all 100 users from the JSON file")
.WithTags("Users")
.Produces<List<Csharp14AnddotNetCoreGoal.Models.User>>(200);

// GET /api/users/{id} - Get user by ID
app.MapGet("/api/users/{id:int}", async (int id, UserService userService) =>
{
    var user = await userService.GetUserByIdAsync(id);
    
    // Using null-conditional with pattern matching
    return user is not null 
        ? Results.Ok(user) 
        : Results.NotFound(new { Message = $"User with ID {id} not found" });
})
.WithName("GetUserById")
.WithDescription("Retrieves a specific user by their ID")
.WithTags("Users")
.Produces<Csharp14AnddotNetCoreGoal.Models.User>(200)
.Produces(404);

// GET /api/users/search?term={searchTerm} - Search users
app.MapGet("/api/users/search", async (string? term, UserService userService) =>
{
    var users = await userService.SearchUsersAsync(term ?? string.Empty);
    return Results.Ok(new 
    { 
        SearchTerm = term ?? "all",
        Count = users.Count,
        Users = users 
    });
})
.WithName("SearchUsers")
.WithDescription("Searches users by name, email, city, or department (demonstrates extension methods)")
.WithTags("Search & Filter")
.Produces(200);

// GET /api/users/department/{department} - Get users by department
app.MapGet("/api/users/department/{department}", async (string department, UserService userService) =>
{
    var users = await userService.GetUsersByDepartmentAsync(department);
    return Results.Ok(new 
    { 
        Department = department,
        Count = users.Count,
        Users = users 
    });
})
.WithName("GetUsersByDepartment")
.WithDescription("Filters users by department (Engineering, Product, Design, Sales, Marketing, HR, Finance, Operations)")
.WithTags("Search & Filter")
.Produces(200);

// GET /api/users/city/{city} - Get users by city
app.MapGet("/api/users/city/{city}", async (string city, UserService userService) =>
{
    var users = await userService.GetUsersByCityAsync(city);
    return Results.Ok(new 
    { 
        City = city,
        Count = users.Count,
        Users = users 
    });
})
.WithName("GetUsersByCity")
.WithDescription("Filters users by city")
.WithTags("Search & Filter")
.Produces(200);

// GET /api/users/age?min={minAge}&max={maxAge} - Get users by age range
app.MapGet("/api/users/age", async (int? min, int? max, UserService userService) =>
{
    var minAge = min ?? 0;
    var maxAge = max ?? 150;
    
    var users = await userService.GetUsersByAgeRangeAsync(minAge, maxAge);
    return Results.Ok(new 
    { 
        AgeRange = $"{minAge}-{maxAge}",
        Count = users.Count,
        Users = users 
    });
})
.WithName("GetUsersByAgeRange")
.WithDescription("Filters users by age range (e.g., min=25&max=35)")
.WithTags("Search & Filter")
.Produces(200);

// GET /api/users/statistics - Get user statistics
app.MapGet("/api/users/statistics", async (UserService userService) =>
{
    var stats = await userService.GetUserStatisticsAsync();
    return Results.Ok(stats);
})
.WithName("GetUserStatistics")
.WithDescription("Returns statistics about all users (total, average age, department counts, etc.)")
.WithTags("Statistics")
.Produces<UserStatistics>(200);

// GET /api/users/{id}/summary - Get user summary using extension method
app.MapGet("/api/users/{id:int}/summary", async (int id, UserService userService) =>
{
    var user = await userService.GetUserByIdAsync(id);
    
    if (user is null)
        return Results.NotFound(new { Message = $"User with ID {id} not found" });
    
    // Demonstrating extension methods
    return Results.Ok(new 
    {
        Id = user.Id,
        DisplayName = user.DisplayName,
        Summary = user.Summary,
        MaskedEmail = user.GetMaskedEmail(),
        IsSenior = user.IsSenior()
    });
})
.WithName("GetUserSummary")
.WithDescription("Gets a formatted user summary using extension methods (demonstrates C# extension members)")
.WithTags("Users")
.Produces(200)
.Produces(404);

// POST /api/users/reload - Reload users from file
app.MapPost("/api/users/reload", async (UserService userService) =>
{
    await userService.ReloadUsersAsync();
    return Results.Ok(new { Message = "Users reloaded successfully" });
})
.WithName("ReloadUsers")
.WithDescription("Clears the cache and reloads users from the JSON file")
.WithTags("Admin")
.Produces(200);

app.UseAuthorization();

app.MapControllers();

app.Run();
