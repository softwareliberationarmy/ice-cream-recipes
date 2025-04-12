using System.ComponentModel.DataAnnotations;

namespace IceCreamRecipes.API.Models;

/// <summary>
/// Represents a recipe source such as a cookbook, website, or family member.
/// </summary>
public class Source
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    public bool HasPageNumbers { get; set; }

    // Navigation property for recipes from this source
    public virtual ICollection<Recipe>? Recipes { get; set; }
}