using FluentAssertions;
using IceCreamRecipes.API.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;

namespace IceCreamRecipes.Tests.Models;

public class SourceTests
{
    [Fact]
    public void CanCreateSource_WithNameAndHasPageNumbersFlag()
    {
        // Arrange
        string name = "Test Cookbook";
        bool hasPageNumbers = true;

        // Act
        var source = new Source
        {
            Name = name,
            HasPageNumbers = hasPageNumbers
        };

        // Assert
        source.Name.Should().Be(name);
        source.HasPageNumbers.Should().Be(hasPageNumbers);
    }

    [Fact]
    public void SourceProperties_AreCorrectlySavedAndRetrieved()
    {
        // Arrange
        var source = new Source { Name = "Initial Name" }; // Initialize with required property
        string name = "Family Recipe";
        bool hasPageNumbers = false;

        // Act
        source.Name = name;
        source.HasPageNumbers = hasPageNumbers;

        // Assert
        source.Name.Should().Be(name);
        source.HasPageNumbers.Should().Be(hasPageNumbers);
    }

    [Fact]
    public void SourceWithNameExceedingMaxLength_IsInvalid()
    {
        // Arrange
        var source = new Source
        {
            Name = new string('A', 256), // 256 characters exceeds the 255 StringLength limit
            HasPageNumbers = true
        };
        var validationContext = new ValidationContext(source);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(source, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("Name"));
    }

    [Fact]
    public void SourceWithNameAtMaxLength_IsValid()
    {
        // Arrange
        var source = new Source
        {
            Name = new string('A', 255), // 255 characters is exactly at the StringLength limit
            HasPageNumbers = true
        };
        var validationContext = new ValidationContext(source);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(source, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeTrue();
        validationResults.Should().BeEmpty();
    }

    [Fact]
    public void SourceWithoutName_IsInvalid()
    {
        // Arrange - Using a constructor trick to avoid initializing the required name property
        var source = typeof(Source).GetConstructor(System.Type.EmptyTypes).Invoke(null) as Source;
        source.HasPageNumbers = true;
        var validationContext = new ValidationContext(source);
        var validationResults = new List<ValidationResult>();

        // Act
        bool isValid = Validator.TryValidateObject(source, validationContext, validationResults, true);

        // Assert
        isValid.Should().BeFalse();
        validationResults.Should().ContainSingle(r => r.MemberNames.Contains("Name"));
    }
}