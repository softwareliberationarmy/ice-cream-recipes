# Ice Cream Recipes Web App Blueprint and Usage Instructions

This document outlines a step‐by‐step, iterative, vertical slice approach for developing the Ice Cream Recipes project. Each chunk delivers a small but demonstrable piece of functionality spanning the backend, frontend, and database. The blueprint is broken down into micro-steps that are small enough for safe implementation with strong testing, yet big enough to move the project forward.

---

## Table of Contents

1. [Phase 1: Project Setup & Basic Recipe Vertical Slice](#phase-1-project-setup--basic-recipe-vertical-slice)
   - [Chunk 1.1: Initialize the Project Repository and Environment](#chunk-11-initialize-the-project-repository-and-environment)
   - [Chunk 1.2: Build a Minimal Recipe CRUD (Vertical Slice)](#chunk-12-build-a-minimal-recipe-crud-vertical-slice)
2. [Phase 2: Advanced Recipe Features & Ingredient Parsing](#phase-2-advanced-recipe-features--ingredient-parsing)
   - [Chunk 2.1: Implement Ingredient Data Model and Parser](#chunk-21-implement-ingredient-data-model-and-parser)
   - [Chunk 2.2: Enhance Recipe CRUD with Comments, Tagging, and Source Management](#chunk-22-enhance-recipe-crud-with-comments-tagging-and-source-management)
3. [Phase 3: Search, Filtering, and Pagination](#phase-3-search-filtering-and-pagination)
   - [Chunk 3.1: Ingredient and Source-Based Search with Unit Conversion](#chunk-31-ingredient-and-source-based-search-with-unit-conversion)
   - [Chunk 3.2: Tag-Based Filtering and Sorting with Pagination](#chunk-32-tag-based-filtering-and-sorting-with-pagination)
4. [Phase 4: Import/Export, Logging, CI/CD, and Containerization](#phase-4-importexport-logging-cicd-and-containerization)
   - [Chunk 4.1: Import and Export Recipes (JSON)](#chunk-41-import-and-export-recipes-json)
   - [Chunk 4.2: Logging, Error Handling, and API Documentation](#chunk-42-logging-error-handling-and-api-documentation)
   - [Chunk 4.3: Finalize CI/CD Pipeline and Containerization](#chunk-43-finalize-cicd-pipeline-and-containerization)
5. [Micro-Steps Breakdown](#micro-steps-breakdown)
6. [Usage Instructions](#usage-instructions)

---

## Phase 1: Project Setup & Basic Recipe Vertical Slice

### Chunk 1.1: Initialize the Project Repository and Environment

**Objective:**  
Set up the monorepo structure with separate `/backend` and `/frontend` folders and basic configuration files.

**Steps:**

1. **Repository Setup:**
   - Create a Git repository and add the basic folder structure:
     - `/backend` for the .NET API.
     - `/frontend` for the React application.
   - Add a root-level `README.md` and a `.gitignore` file.
2. **Docker & CI/CD:**
   - Configure Docker Compose with stub services for backend, frontend, and PostgreSQL.
   - Establish initial GitHub Actions workflow files (a simple “hello world” build for both projects).
3. **Database:**
   - Create a placeholder initial database schema for PostgreSQL.

**Demonstrable Progress:**  
A running repository where Docker Compose starts stub containers and the CI workflow executes without errors.

---

### Chunk 1.2: Build a Minimal Recipe CRUD (Vertical Slice)

**Objective:**  
Allow adding and retrieving recipes through a basic API and a simple frontend form.

**Steps:**

#### Backend:

1. Create a Source model with name and has_page_numbers attributes.
2. Create a Recipe model with basic attributes: Name, Source ID, Page Number (optional), and Preparation Time.
3. Scaffold an ASP.NET Core project.
4. Implement a POST `/api/sources` endpoint to handle source creation.
5. Implement a GET `/api/sources` endpoint to list sources.
6. Implement a POST `/api/recipes` endpoint to handle recipe creation.
7. Implement a GET `/api/recipes` endpoint to list recipes.
8. Connect to PostgreSQL using Entity Framework (or another preferred ORM) and create migrations for the Source and Recipe tables.
9. Write xUnit tests for:
   - Creating and retrieving sources.
   - Creating a recipe with source reference.
   - Listing recipes with their associated sources.

#### Frontend:

1. Scaffold a new React application.
2. Create a source management component for adding/editing sources.
3. Create a form component for entering a recipe (fields: Name, Source selection with dropdown, Page Number if applicable, Preparation Time).
4. Create a list component to display recipes with their sources.
5. Wire up the forms to call the corresponding backend API endpoints.
6. Implement error handling and form validations.
7. Write component tests using Jest and React Testing Library to:
   - Validate source management functionality.
   - Validate the recipe form input, including source selection.
   - Validate that the recipe list renders with source information after API call.

**Demonstrable Progress:**  
A UI that shows a list of recipes with their sources and allows new sources and recipes to be added through the frontend, with complete API integration.

---

## Phase 2: Advanced Recipe Features & Ingredient Parsing

### Chunk 2.1: Implement Ingredient Data Model and Parser

**Objective:**  
Extend the recipe model to include ingredients and build the ingredient parser.

**Steps:**

#### Backend:

1. Extend the Recipe model to include a list of Ingredients (each with Quantity, Unit, and Name).
2. Create an API endpoint that accepts multi-line ingredient input.
3. Develop an ingredient parser that splits each line into quantity, unit, and ingredient name.
4. Provide clear error messages for any poorly formatted lines.
5. Write unit tests to cover various ingredient input formats.

#### Frontend:

1. Update the recipe creation form to include a multi-line text area for ingredients.
2. Implement a live preview panel that shows parsed ingredients and highlights parsing errors.
3. Allow users to manually correct any ingredient details if errors occur.

**Demonstrable Progress:**  
Users can input multi-line ingredients and see a parsed preview with appropriate error feedback. All unit tests pass successfully.

---

### Chunk 2.2: Enhance Recipe CRUD with Comments, Tagging, and Source Management

**Objective:**  
Add support for comments, tagging, and advanced source management to the recipe model.

**Steps:**

#### Backend:

1. Update models to support:
   - Comments (one-to-many relation with recipes).
   - Tags (free-form tagging).
   - Enhanced source management (CRUD operations).
2. Develop API endpoints for:
   - Adding comments.
   - Setting tags on recipes.
   - Managing sources (create, update, delete).
3. Implement basic auto-complete functionality for tags and sources (e.g., using in-memory suggestions).
4. Update and add unit tests for these enhanced endpoints.

#### Frontend:

1. Enhance the recipe view to include a comment section.
2. Add UI inputs for tags with auto-complete suggestions.
3. Create a dedicated source management page/modal with CRUD functionality.
4. Update the recipe creation form to allow selecting from existing sources or creating a new one on-the-fly.
5. Update the UI to handle creation and display of comments, tags, and sources.

**Demonstrable Progress:**  
Users are able to add comments and tags to recipes, manage sources independently, and the UI reflects these changes accordingly.

---

## Phase 3: Search, Filtering, and Pagination

### Chunk 3.1: Ingredient and Source-Based Search with Unit Conversion

**Objective:**  
Allow users to search recipes by ingredients, sources, and compare quantities using proper unit conversion.

**Steps:**

#### Backend:

1. Implement a search endpoint that accepts ingredient parameters (name and quantity constraints) and source filters.
2. Add logic for unit conversion (e.g., from cups to tablespoons) for accurate comparisons.
3. Write integration tests covering the search, filtering, and unit conversion logic.

#### Frontend:

1. Create search controls to filter recipes by:
   - Ingredient and quantity comparisons
   - Source (with dropdown of available sources)
2. Display the filtered list based on the search query.

**Demonstrable Progress:**  
Users can perform ingredient and source-based searches and see a correct list of filtered recipes.

---

### Chunk 3.2: Tag-Based Filtering and Sorting with Pagination

**Objective:**  
Support filtering by tags, sorting options, and implement pagination (50 recipes per page).

**Steps:**

#### Backend:

1. Extend the search endpoint to allow filtering recipes by tags (ensuring that all selected tags match).
2. Add parameters for sorting (by name, preparation time, or source) and implement pagination.
3. Write the necessary unit/integration tests.

#### Frontend:

1. Update the search UI with a tag selector and sorting dropdown.
2. Implement pagination controls on the recipe list view.
3. Ensure proper interaction with the backend search endpoint.

**Demonstrable Progress:**  
The UI displays recipes filtered by tags, sorted as specified, and paginated with a page size of 50.

---

## Phase 4: Import/Export, Logging, CI/CD, and Containerization

### Chunk 4.1: Import and Export Recipes (JSON)

**Objective:**  
Enable users to import recipes from and export recipes to JSON.

**Steps:**

#### Backend:

1. Implement an endpoint to export existing recipes (including ingredients, comments, and tags) as JSON.
2. Add an endpoint to import recipes from a JSON file, with proper validation and error handling.
3. Write tests to cover both the import and export functionalities.

#### Frontend:

1. Create UI buttons for import and export actions.
2. Display success or error messages based on the operation result.

**Demonstrable Progress:**  
Users can successfully export recipes to a JSON file and import recipes from a JSON file.

---

### Chunk 4.2: Logging, Error Handling, and API Documentation

**Objective:**  
Enhance error messaging and integrate logging and API documentation.

**Steps:**

#### Backend:

1. Integrate Serilog for logging and configure it to forward logs to a Seq server.
2. Enhance API error responses, especially for parsing and validation errors.
3. Add Swagger/OpenAPI documentation for all API endpoints.
4. Write tests to verify that logging functions as expected during error scenarios.

#### Frontend:

1. Update error message displays to be more user friendly based on the enhanced backend responses.

**Demonstrable Progress:**  
The API now provides detailed, logged error messages and interactive Swagger documentation is available at a designated endpoint.

---

### Chunk 4.3: Finalize CI/CD Pipeline and Containerization

**Objective:**  
Complete the CI/CD integration and containerize all services.

**Steps:**

#### CI/CD:

1. Finalize the GitHub Actions workflows to run unit, integration, and end-to-end (Cypress) tests.
2. Ensure Docker images are built and pushed (if needed) via the CI pipeline.

#### Containerization:

1. Refine Docker Compose configuration to orchestrate the backend, frontend, PostgreSQL, and Seq services.
2. Test deployment on the NAS server environment.

**Demonstrable Progress:**  
The entire project is containerized and deployed using Docker Compose, with automated testing in the CI/CD pipeline.

---

## Micro-Steps Breakdown

### Example: Micro-Steps for Chunk 1.2 (Minimal Recipe CRUD)

#### Backend Setup:

1. Create the Source model (Name, HasPageNumbers).
2. Create the Recipe model (Name, Source ID, Page Number, PreparationTime).
3. Scaffold an ASP.NET Core project.
4. Implement a POST `/api/sources` endpoint.
5. Implement a GET `/api/sources` endpoint.
6. Implement a POST `/api/recipes` endpoint.
7. Implement a GET `/api/recipes` endpoint.
8. Connect to PostgreSQL and create migrations for the Source and Recipe tables.
9. Write xUnit tests to:
   - Test source creation and retrieval.
   - Test recipe creation with source reference.
   - Test recipe listing with source information.

#### Frontend Setup:

1. Scaffold a React application.
2. Create components for:
   - Source management (add/edit sources).
   - Recipe entry form (fields: Name, Source selection, Page Number, PreparationTime).
   - Recipe list (display recipes with sources).
3. Wire the forms to call the backend API.
4. Write Jest tests to:
   - Validate source management functionality.
   - Validate recipe form input, including source selection.
   - Validate the recipe list renders with source information.

#### Integration:

1. Run both the backend and frontend.
2. Use Postman or a browser to verify that sources and recipes update correctly.

#### Testing:

1. Run all unit tests for the API and the frontend.
2. Confirm that each test passes.
3. Commit the changes.

### Example: Micro-Steps for Source Management

#### Backend Source Management:

1. Create the Source model (Name, HasPageNumbers).
2. Implement CRUD API endpoints for sources.
3. Connect to PostgreSQL and create a migration for the Source table.
4. Update the Recipe model to reference Source.
5. Write xUnit tests for source management operations.

#### Frontend Source Management:

1. Create components for:
   - Source list
   - Source creation/edit form
   - Source selector in recipe form
2. Implement conditional display of page number field based on source selection.
3. Write Jest tests to validate source management functionality.

#### Integration:

1. Run both the backend and frontend.
2. Verify that sources can be created, selected in recipes, and displayed properly.

#### Testing:

1. Run all unit tests for the API and frontend components.
2. Confirm that each test passes.
3. Commit the changes.

**General Guidelines for Each Chunk:**

- Write tests first (TDD approach) where applicable.
- Deliver a working feature that can be demoed (via UI interaction or API response).
- Refactor and document code after each iteration.
- Iterate by adding new vertical slices, then repeat micro-steps for the new feature.

---

## Usage Instructions

1. **Review the Blueprint:**  
   Read through this document to understand the overall project structure and individual feature chunks.

2. **Plan Your Work:**  
   Pick a chunk to work on and follow the detailed steps provided within that section. Start with small, demonstrable vertical slices that integrate backend, frontend, and database changes.

3. **Implement Using TDD:**  
   Write tests first for each new piece of functionality, implement the feature, and then refactor as needed.

4. **Regularly Commit Changes:**  
   Make small, frequent commits that reflect the completion of each micro-step or chunk.

5. **Leverage CI/CD:**  
   Use GitHub Actions and Docker Compose to test and deploy your features continuously.

6. **Iterate and Enhance:**  
   After completing each vertical slice, iterate by adding additional features as guided by the subsequent chunks.

Use this file throughout development to keep track of progress and ensure robust, iterative implementation of the project.
