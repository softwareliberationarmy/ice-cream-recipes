# Frontend Development Guide

This document provides detailed guidance for working on the frontend portion of the Ice Cream Recipes application.

## Technology Stack

- **Framework**: React with TypeScript
- **Build Tool**: Vite
- **Package Manager**: Yarn
- **CSS Framework**: Beer CSS
- **Testing Library**: Jest and React Testing Library
- **End-to-End Testing**: Cypress

## Project Structure

```
frontend/
├── public/           # Static assets
├── src/
│   ├── components/   # Reusable UI components
│   ├── hooks/        # Custom React hooks
│   ├── pages/        # Page components
│   ├── services/     # API services and utilities
│   ├── types/        # TypeScript interfaces and types
│   ├── utils/        # Utility functions
│   ├── App.tsx       # Main application component
│   ├── App.test.tsx  # Tests for App component
│   ├── index.css     # Global styles
│   └── main.tsx      # Application entry point
├── vite.config.ts    # Vite configuration
├── tsconfig.json     # TypeScript configuration
└── package.json      # Project dependencies and scripts
```

## Coding Standards

### Component Structure

1. Use function components (not arrow function syntax):

```tsx
// Correct
function RecipeCard(props: RecipeCardProps) {
  return <div>...</div>;
}

// Avoid
const RecipeCard = (props: RecipeCardProps) => {
  return <div>...</div>;
};
```

2. Define component props with TypeScript interfaces:

```tsx
interface RecipeCardProps {
  recipe: Recipe;
  onEdit?: (id: string) => void;
  onDelete?: (id: string) => void;
}

function RecipeCard(props: RecipeCardProps) {
  // Component implementation
}
```

3. Place interfaces at the top of component files (do not export interfaces for props).

4. Use default exports for components:

```tsx
export default RecipeCard;
```

### Hooks

1. Create custom hooks for complex logic:

```tsx
// useRecipeSearch.ts
function useRecipeSearch(initialFilters: SearchFilters) {
  const [filters, setFilters] = useState<SearchFilters>(initialFilters);
  const [results, setResults] = useState<Recipe[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  // Implementation...

  return { filters, setFilters, results, loading };
}
```

### Testing

1. Place test files alongside the components they test:

```
components/
├── RecipeCard/
│   ├── RecipeCard.tsx
│   └── RecipeCard.test.tsx
```

2. Use React Testing Library to test components:

```tsx
import { render, screen, fireEvent } from '@testing-library/react';
import RecipeCard from './RecipeCard';

describe('RecipeCard', () => {
  it('renders recipe title', () => {
    render(<RecipeCard recipe={{ id: '1', title: 'Vanilla Ice Cream' }} />);
    expect(screen.getByText('Vanilla Ice Cream')).toBeInTheDocument();
  });
});
```

## Key Components and Features

### 1. Recipe Management

- **RecipeList**: Displays paginated recipes with sorting and filtering
- **RecipeForm**: Form for creating and editing recipes
- **RecipeDetail**: Detailed view of a single recipe

### 2. Ingredient Parser

The ingredient parser is a key feature that:

- Takes multi-line text input of ingredients
- Parses each line into quantity, unit, and ingredient name
- Displays a live preview of parsed ingredients
- Allows for correction of parsing errors

Implementation:

```tsx
function IngredientParser({ value, onChange }) {
  const [parsedIngredients, setParsedIngredients] = useState([]);
  const [parseErrors, setParseErrors] = useState([]);

  // Parse ingredients on value change
  useEffect(() => {
    const { ingredients, errors } = parseIngredients(value);
    setParsedIngredients(ingredients);
    setParseErrors(errors);
  }, [value]);

  // Component implementation...
}
```

### 3. Search & Filtering

The application includes advanced search and filtering:

- Ingredient-based search with quantity comparison
- Source filtering with autocomplete
- Tag-based filtering
- Sort controls for recipe name, preparation time, and source

## API Integration

The frontend communicates with the backend API for all data operations:

```tsx
// Example API service
const RecipeService = {
  getAll: async (params: SearchParams): Promise<ApiResponse<Recipe[]>> => {
    return await apiClient.get('/recipes', { params });
  },

  getById: async (id: string): Promise<ApiResponse<Recipe>> => {
    return await apiClient.get(`/recipes/${id}`);
  },

  create: async (recipe: RecipeCreate): Promise<ApiResponse<Recipe>> => {
    return await apiClient.post('/recipes', recipe);
  },

  // Other CRUD operations...
};
```

## Development Workflow

1. **Start the development server**:

   ```
   yarn dev
   ```

2. **Run tests during development**:

   ```
   yarn test --watch
   ```

3. **Build for production**:

   ```
   yarn build
   ```

4. **Preview production build**:
   ```
   yarn preview
   ```

## Common Issues and Solutions

### CORS Issues

If encountering CORS errors when connecting to the backend:

1. Ensure the backend is running and properly configured
2. Check that the API URLs in the frontend use the correct port and protocol
3. Verify CORS headers are properly set in the backend

### Hot Reload Not Working

If changes aren't reflected immediately:

1. Check the terminal for errors
2. Restart the development server
3. Clear the browser cache

## Recommended Extensions for VS Code

- ESLint
- Prettier
- vscode-styled-components
- Jest
- TypeScript Importer
