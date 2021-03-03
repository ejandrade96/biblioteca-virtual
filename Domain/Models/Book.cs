namespace Domain.Models
{
  public class Book : EntityBase
  {
    public string Title { get; protected set; }
    
    public string Author { get; protected set; }
    
    public string ISBN { get; protected set; }
    
    public int Pages { get; protected set; }
    
    public int Edition { get; protected set; }

    public Book(string title, string author, string isbn, int pages, int edition)
    {
      Title = title;
      Author = author;
      ISBN = isbn;
      Pages = pages;
      Edition = edition;
    }
  }
}