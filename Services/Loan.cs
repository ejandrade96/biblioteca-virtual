using System;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.Errors;
using Models = Domain.Models;

namespace Services
{
  public class Loan : ILoan
  {
    private readonly ILoans _loans;

    private readonly ILog _logService;

    private readonly IStudent _studentService;

    private readonly IBook _bookService;

    public Loan(ILoans loans, ILog logService, IStudent studentService, IBook bookService)
    {
      _loans = loans;
      _logService = logService;
      _studentService = studentService;
      _bookService = bookService;
    }

    public IResponse<Domain.Models.Loan> Add(int studentId, int bookId)
    {
      var response = new Response<Models.Loan>();
      var responseStudent = _studentService.Get(studentId);

      if (responseStudent.HasError())
      {
        response.Error = responseStudent.Error;
        return response;
      }

      var responseBook = _bookService.GetWithLoans(bookId);

      if (responseBook.HasError())
      {
        response.Error = responseBook.Error;
        return response;
      }

      if (responseBook.Result.IsBorrowed())
      {
        response.Error = new Error("Livro indisponível para empréstimo!");
        return response;
      }

      var loan = new Models.Loan(responseStudent.Result, responseBook.Result);

      var addedLoan = _loans.Add(loan);
      response.Result = addedLoan;
      _logService.Add(LogType.Create, "empréstimo", addedLoan.Id);

      return response;
    }

    public IResponse<Models.Loan> RegisterBookReturn(int loanId)
    {
      var response = new Response<Models.Loan>();

      var loan = _loans.Get(loanId);

      if (loan == null)
        response.Error = new ErrorObjectNotFound("Empréstimo");

      else if (loan.ReturnDate != null && loan.ReturnDate != DateTime.MinValue)
        response.Error = new Error("Já foi registrado a devolução para este empréstimo!");

      else
      {
        loan.RegisterReturn();
        _loans.Update(loan);
        _logService.Add(LogType.Create, "devolução do empréstimo", loan.Id);
      }

      return response;
    }
  }
}