-- Initial schema for Ice Cream Recipes application

-- Create database (run this separately if not already created)
-- CREATE DATABASE ice_cream_recipes;

-- Units of measure enum
CREATE TYPE measurement_unit AS ENUM (
    'cup', 'tablespoon', 'teaspoon', 'pinch', 
    'fluid_ounce', 'ounce', 'milliliter', 
    'liter', 'gram', 'pound', 'unit'
);

-- Source table for recipe sources
CREATE TABLE source (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    has_page_numbers BOOLEAN NOT NULL DEFAULT FALSE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX idx_source_name ON source(name);

-- Recipe table
CREATE TABLE recipe (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    source_id INTEGER REFERENCES source(id),
    page_number INTEGER,
    preparation_time_minutes INTEGER,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX idx_recipe_name ON recipe(name);
CREATE INDEX idx_recipe_source_id ON recipe(source_id);

-- Ingredient table (master list of ingredients)
CREATE TABLE ingredient (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX idx_ingredient_name ON ingredient(name);

-- Recipe ingredient junction table
CREATE TABLE recipe_ingredient (
    id SERIAL PRIMARY KEY,
    recipe_id INTEGER NOT NULL REFERENCES recipe(id) ON DELETE CASCADE,
    ingredient_id INTEGER NOT NULL REFERENCES ingredient(id),
    quantity DECIMAL(10, 3),
    unit measurement_unit,
    display_text VARCHAR(255) NOT NULL, -- Original parsed text for display
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(recipe_id, ingredient_id, quantity, unit)
);
CREATE INDEX idx_recipe_ingredient_recipe_id ON recipe_ingredient(recipe_id);
CREATE INDEX idx_recipe_ingredient_ingredient_id ON recipe_ingredient(ingredient_id);

-- Comment table
CREATE TABLE comment (
    id SERIAL PRIMARY KEY,
    recipe_id INTEGER NOT NULL REFERENCES recipe(id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX idx_comment_recipe_id ON comment(recipe_id);

-- Tag table
CREATE TABLE tag (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);
CREATE INDEX idx_tag_name ON tag(name);

-- Recipe tag junction table
CREATE TABLE recipe_tag (
    recipe_id INTEGER NOT NULL REFERENCES recipe(id) ON DELETE CASCADE,
    tag_id INTEGER NOT NULL REFERENCES tag(id) ON DELETE CASCADE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (recipe_id, tag_id)
);
CREATE INDEX idx_recipe_tag_tag_id ON recipe_tag(tag_id);

-- Function to update timestamp on row updates
CREATE OR REPLACE FUNCTION update_modified_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = now();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Triggers for updating timestamps
CREATE TRIGGER update_recipe_modtime
BEFORE UPDATE ON recipe
FOR EACH ROW EXECUTE FUNCTION update_modified_column();

CREATE TRIGGER update_source_modtime
BEFORE UPDATE ON source
FOR EACH ROW EXECUTE FUNCTION update_modified_column();

CREATE TRIGGER update_recipe_ingredient_modtime
BEFORE UPDATE ON recipe_ingredient
FOR EACH ROW EXECUTE FUNCTION update_modified_column();

CREATE TRIGGER update_comment_modtime
BEFORE UPDATE ON comment
FOR EACH ROW EXECUTE FUNCTION update_modified_column();
