using FluentAssertions;
using IceCreamRecipes.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;

namespace IceCreamRecipes.Tests.Models;

public class RecipeTests
{
    [Fact]
    public void CanCreateRecipe_WithAllProperties()
    {
        // Arrange
        string name = "Vanilla Ice Cream";
        int sourceId = 1;
        int? pageNumber = 42;
        int preparationTime = 60;

        // Act
        var recipe = new Recipe
        {
            Name = name,
            SourceId = sourceId,
            PageNumber = pageNumber,
            PreparationTime = preparationTime
        };

        // Assert
        recipe.Name.Should().Be(name);
        recipe.SourceId.Should().Be(sourceId);
        recipe.PageNumber.Should().Be(pageNumber);
        recipe.PreparationTime.Should().Be(preparationTime);
    }

    [Fact]
    public void RecipeWithoutPageNumber_IsValid()
    {
        // Arrange & Act
        var recipe = new Recipe
        {
            Name = "Chocolate Ice Cream",
            SourceId = 1,
            PageNumber = null,
            PreparationTime = 45
        };

        // Assert
        recipe.PageNumber.Should().BeNull();
    }

    [Fact]
    public void Recipe_CanBeAssociatedWithSource()
    {
        // Arrange
        var source = new Source
        {
            Id = 1,
            Name = "Ice Cream Cookbook",
            HasPageNumbers = true
        };

        var recipe = new Recipe
        {
            Id = 1,
            Name = "Strawberry Ice Cream",
            SourceId = source.Id,
            PageNumber = 24,
            PreparationTime = 30,
            Source = source
        };

        // Act & Assert
        recipe.Source.Should().BeSameAs(source);
        recipe.SourceId.Should().Be(source.Id);
    }

    [Fact]
    public void RecipeWithNameExceedingMaxLength_IsInvalid()
    {
        // Arrange
        var recipe = new Recipe
        {
            Name = new string('A', 256), // 256 characters exceeds the 255 StringLength limit
            SourceId = 1,
            PreparationTime = 30
        };
        var validationContext = new ValidationContext(recipe);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(recipe, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void RecipeWithNameAtMaxLength_IsValid()
    {
        // Arrange
        var recipe = new Recipe
        {
            Name = new string('A', 255), // 255 characters is exactly at the StringLength limit
            SourceId = 1,
            PreparationTime = 30
        };
        var validationContext = new ValidationContext(recipe);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(recipe, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void RecipeWithoutName_IsInvalid()
    {
        // Arrange - Using a constructor trick to avoid initializing the required name property
        var recipe = typeof(Recipe).GetConstructor(System.Type.EmptyTypes).Invoke(null) as Recipe;
        recipe.SourceId = 1;
        recipe.PreparationTime = 30;
        var validationContext = new ValidationContext(recipe);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(recipe, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void RecipeWithZeroPreparationTime_IsInvalid()
    {
        // Arrange
        var recipe = new Recipe
        {
            Name = "Ice Cream Recipe",
            SourceId = 1,
            PreparationTime = 0 // Zero is invalid per the Range attribute
        };
        var validationContext = new ValidationContext(recipe);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(recipe, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("PreparationTime"));
    }

    [Fact]
    public void RecipeWithNegativePreparationTime_IsInvalid()
    {
        // Arrange
        var recipe = new Recipe
        {
            Name = "Ice Cream Recipe",
            SourceId = 1,
            PreparationTime = -10 // Negative value is invalid per the Range attribute
        };
        var validationContext = new ValidationContext(recipe);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(recipe, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("PreparationTime"));
    }
}