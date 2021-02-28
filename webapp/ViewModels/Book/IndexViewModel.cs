using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Book
{
  public class IndexViewModel
  {
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string ISBN { get; set; }

    [Required]
    public int Pages { get; set; }

    [Required]
    public int Edition { get; set; }

    public bool Borrowed { get; set; }

    public IndexViewModel()
    {
    }
  }
}