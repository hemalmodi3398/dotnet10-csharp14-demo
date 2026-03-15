# ?? Swagger UI Quick Guide

## What is Swagger UI?

Swagger UI is an interactive API documentation tool that lets you test your API directly from your browser without writing any code!

## Accessing Swagger

1. **Run the project:**
   ```bash
   dotnet run
   ```

2. **Open your browser to:**
   ```
   https://localhost:7075
   ```

3. **You'll see the Swagger UI interface!**

## Using Swagger UI

### ?? Overview

When you open Swagger, you'll see:
- **API Title**: "User Management API - C# 14 & .NET 9 Demo"
- **Description**: Lists all the C# features demonstrated
- **Endpoint Groups** (Tags):
  - ?? **Users** - Basic user operations
  - ?? **Search & Filter** - Search and filtering endpoints
  - ?? **Statistics** - User statistics
  - ?? **Admin** - Admin operations

### ?? Testing an Endpoint

#### Example: Get All Users

1. **Find the endpoint**: Look for `GET /api/users` under the **Users** tag
2. **Expand it**: Click on the endpoint row
3. **Try it out**: Click the "Try it out" button (top right of the expanded section)
4. **Execute**: Click the blue "Execute" button
5. **See results**: Scroll down to see:
   - **Curl command** - The equivalent curl command
   - **Request URL** - The actual URL called
   - **Response body** - The JSON response (all 100 users!)
   - **Response headers** - HTTP headers

#### Example: Search Users

1. **Find**: `GET /api/users/search` under **Search & Filter**
2. **Expand it** and click "Try it out"
3. **Enter parameter**: In the `term` field, type: `engineer`
4. **Execute**: Click the blue "Execute" button
5. **See results**: All users matching "engineer" in their profile

#### Example: Get User by ID

1. **Find**: `GET /api/users/{id}` under **Users**
2. **Expand it** and click "Try it out"
3. **Enter ID**: In the `id` field, type: `1`
4. **Execute**: Click the blue "Execute" button
5. **See results**: Details for user with ID 1 (John Smith)

### ?? Understanding the Response

Each response shows:
- **Code**: HTTP status code (200 = success, 404 = not found)
- **Body**: The JSON data returned
- **Headers**: HTTP response headers

Example successful response (200):
```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@company.com",
  "fullName": "John Smith",
  ...
}
```

### ?? Available Endpoints in Swagger

#### Users
- `GET /api/users` - Get all 100 users
- `GET /api/users/{id}` - Get a specific user
- `GET /api/users/{id}/summary` - Get user summary with extension methods

#### Search & Filter
- `GET /api/users/search` - Search users by term
- `GET /api/users/department/{department}` - Filter by department
- `GET /api/users/city/{city}` - Filter by city
- `GET /api/users/age` - Filter by age range (parameters: min, max)

#### Statistics
- `GET /api/users/statistics` - Get comprehensive statistics

#### Admin
- `POST /api/users/reload` - Reload users from JSON file

### ?? Quick Test Ideas

Try these in Swagger UI:

1. **Get all users**: `GET /api/users`
2. **Get user #1**: `GET /api/users/1`
3. **Search for engineers**: `GET /api/users/search?term=engineer`
4. **Get Engineering department**: `GET /api/users/department/Engineering`
5. **Get users in New York**: `GET /api/users/city/New York`
6. **Get users aged 25-35**: `GET /api/users/age?min=25&max=35`
7. **Get statistics**: `GET /api/users/statistics`
8. **Get user summary**: `GET /api/users/1/summary` (shows extension methods!)

### ?? Learning Features Through Swagger

#### Extension Methods
Test `GET /api/users/{id}/summary` to see extension methods in action:
- `GetDisplayName()` - Formats name with job title
- `GetSummary()` - Creates a formatted summary
- `GetMaskedEmail()` - Masks email for privacy
- `IsSenior()` - Checks if user is 40+ years old

#### Pattern Matching
Try `GET /api/users/999` (non-existent user) to see the pattern matching with `is not null`

#### Null-Conditional Assignment
The first request to any endpoint loads data from JSON (slower), subsequent requests use cached data (faster) - demonstrating `??=` operator

### ?? Troubleshooting

**Browser shows "Can't reach this page"?**
- Make sure the app is running (`dotnet run`)
- Check the console for the actual port number
- Try HTTP version: http://localhost:5062

**SSL Certificate error?**
- Run: `dotnet dev-certs https --trust`
- Restart your browser

**No data showing?**
- Check that `Data/users.json` exists
- Try the reload endpoint: `POST /api/users/reload`

### ?? Benefits of Swagger UI

? **No coding required** - Test everything from the browser
? **Interactive** - See results immediately
? **Documentation** - Each endpoint is documented
? **Type safety** - Shows expected parameters and response types
? **Try different values** - Experiment with different inputs
? **Copy curl commands** - Get the equivalent command-line version

---

**Pro Tip**: Keep Swagger UI open while exploring the code to see how the C# features translate to API behavior!

Enjoy testing your API! ??
