using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Student
{
  public class IndexViewModel
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Telephone { get; set; }

    [Required]
    public int Record { get; set; }

    public IndexViewModel()
    {
    }
  }
}