# React coding standards

- Define React components using function syntax instead of arrow syntax
- Prefer default exports to named exports
- Create an interface at the top of a React component file for the React component props. Do not export that interface.
- Use react hooks to encapsulate logic involving several smaller hooks
- Write React components to be thoroughly testable
- Define unit test files for components in the same folder as the component.
- When a component begins to grow and get more responsibilities, try to find ways to extract cohesive code out into smaller components or hooks. When you notice a refactoring opportunity like this, suggest it in our conversation.

# Environment and Tools

- Prefer yarn instead of npm
- Use vite when creating a new project, unless there is a specific mention of 'webpack' or 'module federation' in the project specs

# Unit Testing

- Use Jest and React Testing Library
- Use Jest mocks to isolate the code under test
