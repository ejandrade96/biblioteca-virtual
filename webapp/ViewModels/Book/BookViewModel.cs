using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Book
{
  public class BookViewModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Título é obrigatório.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Autor é obrigatório.")]
    public string Author { get; set; }

    [Required(ErrorMessage = "ISBN é obrigatório.")]
    public string ISBN { get; set; }

    [Required(ErrorMessage = "Páginas é obrigatório.")]
    public int Pages { get; set; }

    [Required(ErrorMessage = "Edição é obrigatória.")]
    public int Edition { get; set; }

    public bool Borrowed { get; set; }
  }
}