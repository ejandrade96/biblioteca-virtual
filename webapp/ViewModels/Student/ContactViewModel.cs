using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Student
{
  public class ContactViewModel
  {
    [Required(ErrorMessage="E-mail é obrigatório.")]
    public string Email { get; set; }

    [Required(ErrorMessage="Telefone é obrigatório.")]
    public string Telephone { get; set; }

    public string CellPhone { get; set; }
  }
}