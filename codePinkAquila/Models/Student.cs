#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace codePinkAquila.Models;

public class Student
{
    [Key]
    public int StudentId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "First name needs to be at least 2 characters.")]
    public string FirstName { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Last name needs to be at least 2 characters.")]
    public string LastName { get; set; }


    public string Nickname { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public string SchoolIdImage { get; set; }

    [Required]
    public string ParentIdImage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}