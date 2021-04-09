using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Student
{
  public class StudentViewModel
  {
    public int Id { get; set; }

    [Required(ErrorMessage="Nome é obrigatório.")]
    public string Name { get; set; }

    [Required(ErrorMessage="Login é obrigatório.")]
    public string Login { get; set; }

    public ContactViewModel Contact { get; set; }

    public AddressViewModel Address { get; set; }

    public int Record { get; set; }

    public StudentViewModel()
    {
    }
  }
}