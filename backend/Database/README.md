# Database Setup and Manual Testing

This directory contains the PostgreSQL database schema for the Ice Cream Recipes application.

## Manual Testing

To verify the SQL script runs successfully in a dockerized PostgreSQL environment:

1. Make sure Docker is installed and running on your system.

2. From this directory, run:

```bash
docker-compose up -d
```

3. Connect to the PostgreSQL instance:

```bash
docker exec -it ice-cream-recipes-db psql -U icecream -d ice_cream_recipes
```

4. Verify the tables were created correctly:

```sql
\dt
```

5. Inspect each table structure:

```sql
\d source
\d recipe
\d ingredient
\d recipe_ingredient
\d comment
\d tag
\d recipe_tag
```

6. Try inserting and querying sample data:

```sql
-- Insert a source
INSERT INTO source (name, has_page_numbers)
VALUES ('Homemade Cookbook', TRUE);

-- Insert a recipe
INSERT INTO recipe (name, source_id, page_number, preparation_time_minutes)
VALUES ('Vanilla Ice Cream', 1, 42, 45);

-- Insert ingredients
INSERT INTO ingredient (name) VALUES
('heavy cream'),
('whole milk'),
('granulated sugar'),
('vanilla extract');

-- Get the recipe ID
SELECT id FROM recipe WHERE name = 'Vanilla Ice Cream';

-- Associate ingredients with the recipe (use the actual recipe ID)
INSERT INTO recipe_ingredient (recipe_id, ingredient_id, quantity, unit, display_text)
VALUES
(1, 1, 2, 'cup', '2 cups heavy cream'),
(1, 2, 1, 'cup', '1 cup whole milk'),
(1, 3, 0.75, 'cup', '3/4 cup granulated sugar'),
(1, 4, 2, 'tablespoon', '2 tablespoons vanilla extract');

-- Query the recipe with its source and ingredients
SELECT r.name AS recipe_name, s.name AS source_name, r.page_number, r.preparation_time_minutes,
       ri.quantity, ri.unit, i.name AS ingredient_name
FROM recipe r
JOIN source s ON r.source_id = s.id
JOIN recipe_ingredient ri ON r.id = ri.recipe_id
JOIN ingredient i ON ri.ingredient_id = i.id
WHERE r.name = 'Vanilla Ice Cream';
```

7. Clean up when finished:

```bash
docker-compose down
```

## Notes

- The database schema includes tables for source, recipe, ingredient, comment, and tag
- Proper relationships and constraints are established between tables
- Indexes are created for performance optimization on frequently queried columns
- The database includes a custom enum type for measurement units
- Automatic timestamp updates are implemented via triggers
