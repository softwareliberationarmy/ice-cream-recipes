import { render, screen } from '@testing-library/react';
import App from './App';

describe('App component', () => {
  it('renders Ice Cream Recipes header', () => {
    render(<App />);
    const headerElement = screen.getByText('Ice Cream Recipes');
    expect(headerElement).toBeInTheDocument();
  });
});
