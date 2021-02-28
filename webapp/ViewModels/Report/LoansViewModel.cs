using System;

namespace webapp.ViewModels.Report
{
  public class LoansViewModel
  {
    public string Student { get; set; }
    
    public string Book { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public LoansViewModel()
    {
    }
  }
}