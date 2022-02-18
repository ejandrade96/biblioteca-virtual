using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Services
{
  public interface ILoan : IServiceBase<Loan>
  {
    IResponse<Loan> Add(int studentId, int bookId);

    IResponse<Loan> RegisterBookReturn(int loanId);

    IEnumerable<IGroupingResponse<DateTime, Loan>> GetNumberLoansAddedInPeriod(int days);

    IEnumerable<IGroupingResponse<DateTime, Loan>> GetNumberReturnsRecordedInPeriod(int days);
  }
}