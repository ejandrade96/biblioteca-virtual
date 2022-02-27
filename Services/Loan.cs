using System;
using System.Collections.Generic;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using Infrastructure.Errors;
using Infrastructure.Helpers;
using Models = Domain.Models;

namespace Services
{
  public class Loan : ServiceBase<Models.Loan>, ILoan
  {
    private readonly ILoans _loans;

    private readonly ILog _logService;

    private readonly IStudent _studentService;

    private readonly IBook _bookService;

    public Loan(ILoans loans, ILog logService, IStudent studentService, IBook bookService) : base(loans)
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

      return base.GetNumberRecordsAddedInPeriod(x => x.LoanDate >= startDay && x.LoanDate <= endDay, x => x.LoanDate.Date);
    }

    public IEnumerable<IGroupingResponse<DateTime, Models.Loan>> GetNumberReturnsRecordedInPeriod(int days)
    {
      var startDay = DateTime.Now.AddDays(-(days - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      return base.GetNumberRecordsAddedInPeriod(x => x.ReturnDate >= startDay && x.ReturnDate <= endDay, x => x.ReturnDate.Value.Date);
    }

    public IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Loan>> GetNumberLoansAddedInPeriodOfMonths(int months)
    {
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-(months - 1)).FirstDayOfMonth();

      return base.GetNumberRecordsAddedInPeriodOfMonths(x => x.LoanDate >= firstDayOfFirstMonth && x.LoanDate <= DateTime.Now,
                                                        x => new { x.LoanDate.Month, x.LoanDate.Year });
    }

    public IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Loan>> GetNumberReturnsRecordedInPeriodOfMonths(int months)
    {
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-(months - 1)).FirstDayOfMonth();

      return base.GetNumberRecordsAddedInPeriodOfMonths(x => x.ReturnDate >= firstDayOfFirstMonth && x.ReturnDate <= DateTime.Now,
                                                        x => new { x.ReturnDate.Value.Month, x.ReturnDate.Value.Year });
    }
  }
}