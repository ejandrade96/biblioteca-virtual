using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels.Student
{
  public class AddressViewModel
  {
    [Required(ErrorMessage="Cep é obrigatório.")]
    public string ZipCode { get; set; }

    [Required(ErrorMessage="Tipo logradouro é obrigatório.")]
    public string StreetType { get; set; }

    [Required(ErrorMessage="Rua é obrigatória.")]
    public string Street { get; set; }

    [Required(ErrorMessage="Número é obrigatório.")]
    public int Number { get; set; }

    public string Complement { get; set; }

    [Required(ErrorMessage="Bairro é obrigatório.")]
    public string District { get; set; }

    [Required(ErrorMessage="Cidade é obrigatória.")]
    public string City { get; set; }
    
    [Required(ErrorMessage="Estado é obrigatório.")]
    public string State { get; set; }
  }
}