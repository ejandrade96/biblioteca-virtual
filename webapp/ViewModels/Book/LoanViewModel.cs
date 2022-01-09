using System.Collections.Generic;
using System.Linq;
using Domain.Services;

namespace webapp.ViewModels.Book
{
  public class LoanViewModel
  {
    public List<BookLoanViewModel> Books { get; set; }

    public LoanViewModel(IBook service)
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
    }
  }
}