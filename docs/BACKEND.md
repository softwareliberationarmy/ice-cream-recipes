# Backend Development Guide

This document provides detailed guidance for working on the backend portion of the Ice Cream Recipes application.

## Technology Stack

- **Framework**: ASP.NET Core (.NET 8)
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Logging**: Serilog with Seq integration
- **Testing**: xUnit

## Project Structure

```
backend/
├── IceCreamRecipes.sln         # Solution file
├── Database/                   # Database setup and migrations
│   ├── docker-compose.yml      # DB container configuration
│   └── InitialSchema.sql       # Initial database schema
├── src/
│   └── IceCreamRecipes.API/    # Main API project
│       ├── Controllers/        # API endpoints
│       ├── Models/             # Data models and DTOs
│       ├── Services/           # Business logic
│       ├── Repositories/       # Data access layer
│       ├── Middleware/         # Custom middleware
│       ├── Extensions/         # Extension methods
│       ├── Program.cs          # Application entry point
│       └── appsettings.json    # Configuration
└── tests/
    └── IceCreamRecipes.Tests/  # Unit and integration tests
```

## Data Models

The primary data models in the application include:

### Recipe

The Recipe model contains essential information about an ice cream recipe, including name, preparation time, source reference, ingredients, comments, and tags.

### Source

The Source model represents where a recipe comes from, such as a cookbook, website, or family member. It includes a name and an indicator whether the source uses page numbers.

### Ingredient

The Ingredient model represents an item used in a recipe, including its quantity, optional unit of measure, and name.

## Key Features Implementation

### 1. Ingredient Parser

The ingredient parser service will convert multi-line text input into structured ingredient data. It will handle various formats including fractions and different measurement units.

### 2. Unit Conversion

Unit conversion functionality will enable comparing ingredient quantities across different units of measurement (cups, teaspoons, grams, etc.).

### 3. Recipe Search and Filtering

The search service will implement advanced filtering capabilities by ingredients, sources, and tags with support for proper pagination.

## API Endpoints

The API provides the following main endpoints:

### Recipes

- `GET /api/recipes` - Get all recipes (paginated, with filtering and sorting)
- `GET /api/recipes/{id}` - Get a specific recipe by ID
- `POST /api/recipes` - Create a new recipe
- `PUT /api/recipes/{id}` - Update an existing recipe
- `DELETE /api/recipes/{id}` - Delete a recipe

### Sources

- `GET /api/sources` - Get all recipe sources
- `POST /api/sources` - Create a new source
- `PUT /api/sources/{id}` - Update a source
- `DELETE /api/sources/{id}` - Delete a source
- `POST /api/sources/merge` - Merge two sources

### Tags

- `GET /api/tags` - Get all tags
- `GET /api/tags/autocomplete?q={query}` - Get tags for autocomplete

## Error Handling

The API will use a consistent error handling middleware to:

- Log errors appropriately
- Transform exceptions to appropriate HTTP status codes
- Return user-friendly error messages
- Handle specific exception types differently (not found, validation errors, etc.)

## Logging

The application will use Serilog for structured logging with configuration set in the application settings and logs directed to both the console and Seq server.

## Database Management

### Entity Framework Migrations

To create a new migration:

```bash
dotnet ef migrations add MigrationName --project src/IceCreamRecipes.API
```

To update the database:

```bash
dotnet ef database update --project src/IceCreamRecipes.API
```

### Direct SQL Scripts

For more complex database changes, SQL scripts can be added to the Database folder.

## Testing

The backend will use xUnit for testing with a focus on:

- Unit tests for services and controllers
- Integration tests for database operations
- Tests for complex logic like ingredient parsing and unit conversion

## Performance Considerations

1. **Database Indexing**

   - Indexes are defined for frequently queried columns such as recipe names, ingredient names, and tag names.

2. **N+1 Query Prevention**

   - Use of `.Include()` and eager loading to avoid N+1 query problems.

3. **Caching**
   - Consider implementing response caching for frequently accessed resources.

## API Security

The API implements the following security measures:

1. **CORS Policy**

   - Configuration will allow requests from the frontend application with appropriate headers and methods.

2. **Rate Limiting**
   - Consider implementing rate limiting for public endpoints.

## Development Workflow

1. **Run the API locally**:

   ```bash
   cd backend
   dotnet run --project src/IceCreamRecipes.API
   ```

2. **Run tests**:

   ```bash
   cd backend
   dotnet test
   ```

3. **Access Swagger documentation**:
   Browse to `http://localhost:8080/swagger` when the API is running.

4. **Monitor logs**:
   Access Seq at `http://localhost:5341` to view structured logs.
