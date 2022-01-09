using System.Collections.Generic;
using System.Linq;
using Domain.Services;
using Domain.ValueObjects;

namespace webapp.ViewModels.Book
{
  public class LoanViewModel
  {
    public List<BookLoanViewModel> Books { get; set; }

    public List<StudentLoanViewModel> Students { get; set; }

    public LoanViewModel(IBook service, IStudent studentService)
    {
      Books = service.GetAllWithLoansWithStudent().Select(book =>
      {
        return new BookLoanViewModel
        {
          Id = book.Id,
          Title = book.Title,
          Author = book.Author,
          ISBN = book.ISBN,
          Pages = book.Pages,
          Edition = book.Edition,
          LoanStatus = book.IsBorrowed() ? book.GetLoanStudentLogin() : "Disponível"
        };
      }).ToList();

      Students = studentService.GetAll(Status.Active).Select(student => new StudentLoanViewModel
      {
        Id = student.Id,
        Name = student.Name,
        Login = student.Login
      }).ToList();
    }
  }
}