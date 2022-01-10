using Domain.Models;

namespace Domain.Services
{
  public interface ILoan
  {
    IResponse<Loan> Add(int studentId, int bookId);

    IResponse<Loan> RegisterBookReturn(int loanId);
  }
}