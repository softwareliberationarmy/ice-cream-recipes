# Ice-Cream-Recipes Web App Specification

## 1. Overview

**Purpose:**  
A web application to store, search, and manage ice cream recipes aimed primarily at enthusiasts and home cooks. The primary user is an ice cream-making enthusiast.

**Features:**

- Full CRUD operations for recipes and associated comments.
- Advanced ingredient management and search (including filtering by quantity, with unit conversion).
- Free-form tagging with auto-completion support.
- Multi-line ingredient input with intelligent, flexible parsing and preview/correction capabilities.
- Import/export recipes to/from JSON.
- Sorting, pagination (50 recipes per page), and filtering (by recipe source, ingredients, and tags).
- Containerized architecture with CI/CD automation.

---

## 2. Functional Requirements

### 2.1 Recipe Data Model

- **Recipe Attributes:**
  - **Name:** Recipe title.
  - **Source:** Recipe provenance (e.g., book details, page number).
  - **Ingredients:**
    - Each ingredient must store:
      - **Quantity:** Supports whole numbers, decimals, and fractions (e.g., "1/2", "1 1/2").
      - **Unit of Measure:** Supported units include cups, tablespoons, teaspoons, pinch, fluid ounces, ounces, milliliters, liters, grams, and pounds.
      - **Name:** The ingredient name.
    - Should handle ingredients with a quantity but no measurement unit (e.g., "2 bananas").
  - **Comments:** Allows adding comments to recipes later.
  - **Preparation Time:** Specified in minutes.

### 2.2 Ingredient Parsing

- **Input Method:**
  - A multi-line text box for ingredient entry during recipe creation.
- **Parser Requirements:**
  - Automatically parse each line into quantity, unit (if provided), and ingredient name.
  - Handle multiple, flexible formats with no fixed formatting rules.
  - Recognize and convert fractional quantities into a standard numeric format.
- **Feedback and Correction:**
  - Present a preview of parsed ingredients before finalizing the recipe.
  - Provide clear feedback on lines that cannot be parsed.
  - Allow manual adjustments or corrections for any parsing issues.

### 2.3 Search & Filtering

- **Ingredient Search:**
  - Return recipes containing a specified ingredient.
  - Support filtering by multiple ingredients simultaneously.
  - Enable filtering based on ingredient quantity comparisons (e.g., “recipes with less than 2 cups of milk”), with automatic unit conversions.
- **Tag Search:**
  - Free-form tagging with auto-complete to minimize duplicates.
  - Searches by tags must return recipes matching **all** selected tags.
- **Sorting and Filtering:**
  - **Sorting Options:** Recipe name, preparation time, and recipe source.
  - **Filtering Options:** Filter by recipe source.
- **Pagination:**
  - Display recipes in pages with 50 recipes per page.

### 2.4 Additional Functionalities

- **Import/Export:**
  - Provide the ability to import and export recipes in JSON format.
- **API Documentation:**
  - Incorporate Swagger/OpenAPI for interactive API documentation in the .NET backend.
- **Logging:**
  - Use Serilog to log application events, with logs forwarded to a Seq server.
- **Operational Mode:**
  - Designed as a personal single-user application, with potential future support for user authentication and multi-user access.

---

## 3. Architecture and Technology Stack

### 3.1 Project Structure (Monorepo)

- **Directory Layout:**
  - `/backend`: Contains the .NET backend API.
  - `/frontend`: Contains the React application.
- **Shared Assets:**
  - Initially, no shared folder; backend and frontend remain separate.

### 3.2 Backend (.NET)

- **Framework:** ASP.NET Core (.NET).
- **Database:** PostgreSQL.
- **Core API Features:**
  - RESTful endpoints for recipes, ingredients, comments, and tags.
  - Implement unit conversion logic across supported measurement units.
  - Integrate a flexible ingredient parser for multi-line inputs.
  - Include Swagger/OpenAPI support for interactive API documentation.
- **Logging and Error Handling:**
  - Use Serilog with Seq integration for centralized logging.
  - Provide robust error handling with clear, informative HTTP responses, especially for parsing errors.

### 3.3 Frontend (React)

- **Framework:** React.
- **Design Library:** Beer CSS.
- **UI/UX Requirements:**
  - Fully responsive and mobile-friendly design.
  - User interface for managing recipes (list, add, edit, view).
  - Multi-line text box for ingredient input that displays a live preview of parsed results and supports inline error correction.
- **Search & Filtering UI:**
  - Controls for filtering by ingredients (including quantity comparisons) and tags.
  - Sorting and pagination controls (50 recipes per page).

### 3.4 Containerization & CI/CD

- **Containerization:**
  - Containerize the entire application using Docker.
  - Use Docker Compose for orchestrating multiple services (backend, frontend, PostgreSQL, Seq).
- **CI/CD Pipeline:**
  - Implement using GitHub Actions:
    - Build and test the .NET backend and React frontend.
    - Run unit tests (xUnit for .NET, Jest/React Testing Library for React) and end-to-end tests (Cypress).
    - Package the application as Docker images.
    - Deploy via Docker Compose to the NAS server.

---

## 4. Testing Plan

### 4.1 Backend Testing

- **Framework:** xUnit.
- **Testing Scope:**
  - Unit tests for API endpoints (CRUD operations, ingredient parsing, unit conversion).
  - Integration tests for database interactions.
  - Tests for error handling (especially parsing issues).

### 4.2 Frontend Testing

- **Framework:** Jest and React Testing Library.
- **Testing Scope:**
  - Unit tests for components (form validations, search/filter interactions, ingredient preview).
  - Tests for mock API requests and responses.
  - Validate responsiveness and proper UI component behavior.

### 4.3 End-to-End Testing

- **Tool:** Cypress.
- **Testing Scope:**
  - Full workflow tests including recipe creation (testing multi-line ingredient parsing), searching, filtering, and editing.
  - Validate error feedback for unparseable ingredient lines and ensure manual correction functionality.
  - Test pagination, sorting, and tag-based filtering.

### 4.4 Test-Driven Development (TDD)

- **Approach:**
  - Develop features using TDD.
  - Start each feature with failing tests and implement code to pass those tests.

---

## 5. Error Handling and Logging

### 5.1 Error Handling

- **Backend:**
  - Ensure detailed HTTP error responses with meaningful messages for failures.
  - In ingredient parsing, identify problematic lines and provide descriptive errors to the frontend.
- **Frontend:**
  - Display user-friendly error messages.
  - Enable inline correction for ingredients that could not be parsed correctly.

### 5.2 Logging

- **Implementation:**
  - Use Serilog to log key events and errors.
  - Route logs to a Seq server for centralized log management.
  - Ensure logs capture sufficient context for troubleshooting.

---

## 6. Deployment & Containerization

### 6.1 Docker Compose

- **Services to Orchestrate:**
  - .NET Backend Service.
  - React Frontend Service.
  - PostgreSQL Database.
  - Seq Server for log aggregation.
- **Configuration:**
  - Each service will have its own Dockerfile.
  - Orchestration managed via a `docker-compose.yml` file with proper networking and volume setups.

### 6.2 CI/CD Pipeline (GitHub Actions)

- **Pipeline Overview:**
  - Code checkout from the monorepo.
  - Build and run tests for both backend (xUnit) and frontend (Jest/React Testing Library).
  - Execute Cypress end-to-end tests.
  - Build Docker images for each service.
  - Push images to a registry if necessary.
  - Deploy using Docker Compose on the NAS server.

---

## 7. Summary

The **ice-cream-recipes** web app is a monorepo-based, containerized solution with a .NET backend and a React frontend. Key features include robust recipe management with advanced ingredient parsing (supporting flexible formats and fractional quantities), comprehensive search and filter capabilities (by ingredients, tags, recipe source), and full CRUD for recipes and comments. The app will integrate full automated testing using TDD, CI/CD with GitHub Actions, centralized logging via Serilog and Seq, as well as responsive design using Beer CSS. This specification provides the necessary details for a developer to begin immediate implementation.

(created using Copilot o3-mini)
