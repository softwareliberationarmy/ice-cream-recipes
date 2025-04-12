using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IceCreamRecipes.API.Models;

/// <summary>
/// Represents an ice cream recipe with its properties and relationship to its source.
/// </summary>
public class Recipe
{
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public required string Name { get; set; }

    [Required]
    public int SourceId { get; set; }

    [ForeignKey("SourceId")]
    public virtual Source? Source { get; set; }

    public int? PageNumber { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PreparationTime { get; set; }
}