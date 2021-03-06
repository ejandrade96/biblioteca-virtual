using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
  public class Loan : EntityBase
  {
    [Required]
    public virtual Student Student { get; protected set; }
    
    [Required]
    public virtual Book Book { get; protected set; }
    
    public DateTime LoanDate { get; protected set; }

    public DateTime ReturnDate { get; protected set; }
    
    protected Loan()
    {
    }

    public Loan(Student student, Book book, DateTime loanDate, DateTime returnDate)
    {
      Student = student;
      Book = book;
      LoanDate = loanDate;
      ReturnDate = returnDate;
    }
  }
}