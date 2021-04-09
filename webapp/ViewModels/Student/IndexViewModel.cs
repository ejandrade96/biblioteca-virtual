using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace webapp.ViewModels.Student
{
  public class IndexViewModel
  {
    private readonly IStudent _service;

    public StudentViewModel Student { get; set; }

    public List<StudentViewModel> Students { get; set; }

    public IndexViewModel() { }

    public IndexViewModel(IStudent service)
    {
      _service = service;

      Students = _service.GetAll().Select(student => new StudentViewModel
      {
        Id = student.Id,
        Name = student.Name,
        Login = student.Login,
        Record = student.Record,
        Contact = new ContactViewModel { Email = student.Contact.Email, Telephone = student.Contact.Telephone, CellPhone = student.Contact.CellPhone },
        Address = new AddressViewModel
        {
          ZipCode = student.Address.ZipCode,
          StreetType = student.Address.StreetType.Code,
          Street = student.Address.Street,
          Number = student.Address.Number,
          Complement = student.Address.Complement,
          District = student.Address.District,
          City = student.Address.City,
          State = student.Address.State.Acronym
        }
      }).ToList();
    }
  }
}