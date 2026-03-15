# Quick Start & Testing Guide

## Running the Application

1. Open terminal in the project directory
2. Run the application:
   ```bash
   dotnet run
   ```
3. The application will start at:
   - **HTTPS**: https://localhost:7075
   - **HTTP**: http://localhost:5062
4. **Swagger UI** will automatically open in your browser at the root URL (https://localhost:7075)

## Using Swagger UI (Recommended for Testing)

Once the application is running, open your browser to:

**https://localhost:7075**

You'll see a beautiful Swagger UI interface where you can:
- ?? View all API endpoints organized by tags
- ?? See detailed descriptions of each endpoint
- ?? Try out endpoints directly in the browser
- ?? View request/response schemas
- ?? Test different parameters and see results instantly

### Swagger Tips:
1. Click on any endpoint to expand it
2. Click **"Try it out"** button
3. Enter parameters if required
4. Click **"Execute"**
5. See the response below!

## Quick Test Commands

Replace `7000` with your actual port number from the console output (default: 7075 for HTTPS, 5062 for HTTP).

**Note: It's much easier to use Swagger UI! Just open https://localhost:7075 in your browser.**

### Basic Tests

```bash
# 1. Get all users (returns 100 users)
curl https://localhost:7000/api/users | ConvertFrom-Json

# 2. Get first user
curl https://localhost:7000/api/users/1 | ConvertFrom-Json

# 3. Search for engineers
curl "https://localhost:7000/api/users/search?term=engineer" | ConvertFrom-Json

# 4. Get Engineering department users
curl https://localhost:7000/api/users/department/Engineering | ConvertFrom-Json

# 5. Get statistics
curl https://localhost:7000/api/users/statistics | ConvertFrom-Json
```

### PowerShell Testing (Windows)

```powershell
# Set your base URL
$baseUrl = "https://localhost:7000"

# Get all users
Invoke-RestMethod -Uri "$baseUrl/api/users"

# Get user by ID
Invoke-RestMethod -Uri "$baseUrl/api/users/1"

# Search users
Invoke-RestMethod -Uri "$baseUrl/api/users/search?term=john"

# Get Engineering department
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Engineering"

# Get users in New York
Invoke-RestMethod -Uri "$baseUrl/api/users/city/New York"

# Get users aged 25-35
Invoke-RestMethod -Uri "$baseUrl/api/users/age?min=25&max=35"

# Get statistics
Invoke-RestMethod -Uri "$baseUrl/api/users/statistics"

# Get user summary (demonstrates extension methods)
Invoke-RestMethod -Uri "$baseUrl/api/users/1/summary"
```

## Expected Results

### GET /api/users/1
```json
{
  "id": 1,
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@company.com",
  "phoneNumber": "+1-555-0101",
  "city": "New York",
  "country": "USA",
  "department": "Engineering",
  "jobTitle": "Senior Software Engineer",
  "age": 35,
  "fullName": "John Smith",
  "isActive": true
}
```

### GET /api/users/1/summary
```json
{
  "id": 1,
  "displayName": "Senior Software Engineer - John Smith",
  "summary": "John Smith (35 years old) - Senior Software Engineer in Engineering, based in New York, USA",
  "maskedEmail": "j***h@company.com",
  "isSenior": false
}
```

### GET /api/users/statistics
```json
{
  "totalUsers": 100,
  "averageAge": 31.5,
  "departmentCounts": {
    "Engineering": 25,
    "Product": 8,
    "Design": 7,
    ...
  },
  "cityCounts": {
    "New York": 3,
    "San Francisco": 2,
    ...
  },
  "seniorEmployees": 42
}
```

## Features to Notice

1. **Extension Methods in Action**
   - `/api/users/1/summary` - Uses `GetDisplayName()`, `GetSummary()`, `GetMaskedEmail()`, `IsSenior()`

2. **Null-Conditional Assignment**
   - First request loads data from JSON
   - Subsequent requests use cached data (much faster)

3. **Pattern Matching**
   - Try requesting a non-existent user: `/api/users/999`
   - Notice the proper 404 response with message

4. **Minimal APIs**
   - Clean, concise endpoint definitions
   - No controllers needed!

5. **Record Types**
   - Check `UserStatistics` response structure
   - Check `ResponseDtos.cs` for various record patterns

## Testing Different Departments

```powershell
# Try these departments:
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Engineering"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Product"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Design"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Sales"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Marketing"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/HR"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Finance"
Invoke-RestMethod -Uri "$baseUrl/api/users/department/Operations"
```

## Testing Search

```powershell
# Search by first name
Invoke-RestMethod -Uri "$baseUrl/api/users/search?term=john"

# Search by last name
Invoke-RestMethod -Uri "$baseUrl/api/users/search?term=smith"

# Search by department
Invoke-RestMethod -Uri "$baseUrl/api/users/search?term=engineering"

# Search by city
Invoke-RestMethod -Uri "$baseUrl/api/users/search?term=york"
```

## Using a Browser

Simply open these URLs in your browser (adjust port as needed):

1. https://localhost:7000/api/users
2. https://localhost:7000/api/users/1
3. https://localhost:7000/api/users/statistics
4. https://localhost:7000/api/users/search?term=engineer
5. https://localhost:7000/api/users/department/Engineering
6. https://localhost:7000/api/users/1/summary

## OpenAPI/Swagger

While this project includes OpenAPI support, you can view the OpenAPI spec at:
```
https://localhost:7000/openapi/v1.json
```

For a better experience, you could add Swagger UI, but the basic OpenAPI schema is available.

## Troubleshooting

1. **Port already in use?**
   - The app will auto-select a different port
   - Check console output for actual URL

2. **SSL Certificate error?**
   - Run: `dotnet dev-certs https --trust`

3. **File not found error?**
   - Ensure `users.json` is in the `Data` folder
   - The file should be automatically copied during build

4. **Cache issues?**
   - Use the reload endpoint: `POST https://localhost:7000/api/users/reload`

Enjoy exploring modern C# and .NET features! ??
