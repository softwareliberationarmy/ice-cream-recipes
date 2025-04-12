# Ice Cream Recipes (generated using Copilot)

A comprehensive application for managing and sharing ice cream recipes.

## Project Structure

This project is organized as a monorepo with the following structure:

- `/backend` - .NET Core API for managing ice cream recipes data
- `/frontend` - React application for the user interface

## Getting Started

### Prerequisites

- Docker Desktop
- Docker Compose
- .NET Core SDK 8.0 or higher (for development)
- Node.js 20.x or higher
- Yarn

### Running the Application

The easiest way to run the entire application is using Docker Compose, which will start all services:

```bash
# Start all services
docker-compose up

# Or run in detached mode
docker-compose up -d
```

Once running, you can access:

- Frontend application: http://localhost:3000
- Backend API: http://localhost:8080
- Seq logging interface: http://localhost:5341
- PostgreSQL (internal): Port 5432

To stop all services:

```bash
docker-compose down
```

### Development Setup

For local development, you can run each component separately:

#### Backend Development

```bash
cd backend
dotnet build
dotnet run --project src/IceCreamRecipes.API/IceCreamRecipes.API.csproj
```

#### Frontend Development

```bash
cd frontend
yarn install
yarn dev
```

## Features

- Browse ice cream recipes
- Search by ingredients
- Filter by categories
- Create and save your own ice cream recipes
- Rate and comment on recipes

## License

This project is licensed under the MIT License - see the LICENSE file for details.
