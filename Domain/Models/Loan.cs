using System;

namespace Domain.Models
{
  public class Loan : EntityBase
  {
    public Student Student { get; protected set; }
    
    public Book Book { get; protected set; }
    
    public DateTime LoanDate { get; protected set; }

    public DateTime ReturnDate { get; protected set; }
    
    public Loan(Student student, Book book, DateTime loanDate, DateTime returnDate)
    {
      Student = student;
      Book = book;
      LoanDate = loanDate;
      ReturnDate = returnDate;
    }
  }
}