using System.Linq;
using Domain.Models;
using Domain.ValueObjects;
using webapp.ViewModels.Student;

namespace webapp.Helpers
{
  public static class ConversionHelper
  {
    public static Student ToModel(this StudentViewModel viewModel)
    {
      var contact = new Contact(viewModel.Contact.Email, viewModel.Contact.Telephone);
      contact.SetCellPhone(viewModel.Contact.CellPhone);
      var streetType = StreetType.StreetTypes.First(x => x.Code == viewModel.Address.StreetType);
      var state = State.States.First(x => x.Acronym == viewModel.Address.State);
      var address = new Address
      (
        viewModel.Address.ZipCode,
        streetType,
        viewModel.Address.Street,
        viewModel.Address.Number,
        viewModel.Address.District,
        viewModel.Address.City,
        state
      );
      address.SetComplement(viewModel.Address.Complement);
      var student = new Student(viewModel.Name, viewModel.Login, viewModel.Record, contact, address);

      return student;
    }
  }
}