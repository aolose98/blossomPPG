#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace codePinkAquila.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public string Img1 { get; set; }
    public string Img2 { get; set; }
    public string Img3 { get; set; }
    public string Img4 { get; set; }
    public string Img5 { get; set; }
    public string Description { get; set; }

    // public bool AddToCart { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    public int UserId { get; set; }
    public User? Admin { get; set; }
}