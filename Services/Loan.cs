using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.Errors;
using Infrastructure.Helpers;
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

    public IEnumerable<IGroupingResponse<DateTime, Models.Loan>> GetNumberLoansAddedInPeriod(int days)
    {
      var startDay = DateTime.Now.AddDays(-(days - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      return _loans.FindAll(x => x.LoanDate >= startDay && x.LoanDate <= endDay)
                   .ToList()
                   .GroupBy(x => x.LoanDate.Date)
                   .Select(x => new GroupingResponse<DateTime, Models.Loan>
                   {
                     Key = x.Key,
                     Elements = x.ToList()
                   })
                   .OrderBy(x => x.Key);
    }

    public IEnumerable<IGroupingResponse<DateTime, Models.Loan>> GetNumberReturnsRecordedInPeriod(int days)
    {
      var startDay = DateTime.Now.AddDays(-(days - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      return _loans.FindAll(x => x.ReturnDate >= startDay && x.ReturnDate <= endDay)
                   .ToList()
                   .GroupBy(x => x.ReturnDate.Value.Date)
                   .Select(x => new GroupingResponse<DateTime, Models.Loan>
                   {
                     Key = x.Key,
                     Elements = x.ToList()
                   })
                   .OrderBy(x => x.Key);
    }
  }
}