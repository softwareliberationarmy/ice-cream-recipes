# Ice Cream Recipes

A comprehensive web application for ice cream enthusiasts to store, search, and manage their favorite ice cream recipes with advanced ingredient management, tagging, and recipe organization capabilities.

## Project Overview

This application allows users to:

- Create, read, update, and delete ice cream recipes
- Add and manage ingredient lists with flexible parsing
- Search recipes by ingredients, including quantity-based filtering
- Organize recipes with free-form tags and source information
- Import and export recipes in JSON format
- Sort, filter, and paginate through the recipe collection

The project is built with a .NET backend API and React frontend, containerized using Docker for easy deployment.

## Project Structure

This project is organized as a monorepo with the following structure:

- `/backend` - .NET Core API with PostgreSQL database
  - `/src` - API implementation and business logic
  - `/tests` - Backend unit and integration tests
  - `/Database` - Database initialization scripts and documentation
- `/frontend` - React application built with Vite and TypeScript
  - `/src` - React components and application logic
  - `/public` - Static assets

## Prerequisites

- Docker Desktop (for containerized deployment)
- Docker Compose
- .NET SDK 8.0 or higher (for backend development)
- Node.js 20.x or higher (for frontend development)
- Yarn package manager
- PostgreSQL (optional for local database development)
- Git

## Setup Instructions

### Option 1: Running with Docker (Recommended for first run)

The easiest way to get started is using Docker Compose:

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd ice-cream-recipes
   ```

2. Start all services:

   ```bash
   docker-compose up -d
   ```

3. Access the application:

   - Frontend UI: http://localhost:3000
   - Backend API: http://localhost:5000
   - API Documentation (Swagger): http://localhost:5000/swagger
   - Seq logging interface: http://localhost:5341

4. To stop all services:
   ```bash
   docker-compose down
   ```

### Option 2: Local Development Setup

#### Backend Development

1. Navigate to the backend directory:

   ```bash
   cd backend
   ```

2. Start the PostgreSQL and Seq containers:

   ```bash
   cd Database
   docker-compose up -d
   cd ..
   ```

3. Restore and build the solution:

   ```bash
   dotnet restore
   dotnet build
   ```

4. Run the API:

   ```bash
   dotnet run --project src/IceCreamRecipes.API/IceCreamRecipes.API.csproj
   ```

5. The API will be available at:
   - API: http://localhost:8080
   - Swagger documentation: http://localhost:8080/swagger

#### Frontend Development

1. Navigate to the frontend directory:

   ```bash
   cd frontend
   ```

2. Install dependencies:

   ```bash
   yarn install
   ```

3. Start development server:

   ```bash
   yarn dev
   ```

4. The development server will be available at:
   - http://localhost:5173 (default Vite port)

## Development Workflow

### Backend Development

1. Make API changes in the appropriate controllers and models
2. Run tests: `dotnet test`
3. Start the API locally for development or use Docker
4. Check Seq logs at http://localhost:5341 for debugging

### Frontend Development

1. Create/modify React components following project standards:

   - Use function components (not arrow functions)
   - Create interfaces for component props
   - Place test files alongside components
   - Use custom hooks for complex logic

2. Run tests:

   ```bash
   yarn test
   ```

3. Build for production:
   ```bash
   yarn build
   ```

### Making Changes to the Database

1. Add your migration scripts to `/backend/Database`
2. If needed, update the `InitialSchema.sql` file
3. Restart PostgreSQL container to apply changes:
   ```bash
   docker-compose down postgres
   docker-compose up -d postgres
   ```

## Testing Instructions

### Running Backend Tests

```bash
cd backend
dotnet test
```

### Running Frontend Tests

```bash
cd frontend
yarn test
```

### Running End-to-End Tests

```bash
cd frontend
yarn test:e2e
```

## Docker Usage Information

### Rebuilding Containers After Changes

```bash
# Rebuild a specific service
docker-compose build frontend
docker-compose build backend

# Rebuild and restart all services
docker-compose up --build -d
```

### Accessing Container Logs

```bash
# View logs for all containers
docker-compose logs

# View logs for a specific container
docker-compose logs frontend
docker-compose logs backend

# Follow logs in real-time
docker-compose logs -f
```

### Database Access

The PostgreSQL database is accessible:

- From inside containers: `postgres:5432`
- From the host machine: `localhost:5432`
- Connection string: `Host=localhost;Database=ice_cream_recipes;Username=icecream;Password=icecream_pass`

### Container Management

```bash
# View running containers
docker-compose ps

# Stop all containers
docker-compose down

# Stop a specific container
docker-compose stop frontend

# Remove volumes (will delete database data)
docker-compose down -v
```

## Contributing

1. Create a feature branch from `main`
2. Make your changes following project standards
3. Write tests for your changes
4. Ensure all tests pass
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
