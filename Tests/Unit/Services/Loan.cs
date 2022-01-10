using System.Linq;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Errors;
using Moq;
using Services;
using Xunit;
using Models = Domain.Models;
using Service = Services;

namespace Tests.Unit.Services
{
  public class Loan
  {
    private readonly ILoan _service;

    private readonly Mock<ILoans> _loans;

    private readonly Mock<ILog> _logService;

    private readonly Mock<IStudent> _studentService;

    private readonly Mock<IBook> _bookService;

    public Loan()
    {
      _loans = new Mock<ILoans>();
      _logService = new Mock<ILog>();
      _studentService = new Mock<IStudent>();
      _bookService = new Mock<IBook>();
      _service = new Service.Loan(_loans.Object, _logService.Object, _studentService.Object, _bookService.Object);
    }


    [Fact]
    public void Deve_Ser_Possivel_Emprestar_Um_Livro_A_Um_Aluno()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var responseBook = new Response<Models.Book>();
      responseBook.Result = book;

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var responseStudent = new Response<Models.Student>();
      responseStudent.Result = student;

      var loan = new Models.Loan(student, book) { Id = 1 };

      _studentService.Setup(studentService => studentService.Get(1)).Returns(responseStudent);
      _bookService.Setup(bookService => bookService.GetWithLoans(1)).Returns(responseBook);
      _loans.Setup(repository => repository.Add(It.IsAny<Models.Loan>())).Returns(loan);

      var response = _service.Add(1, 1);

      response.Error.Should().BeNull();
      response.Result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Emprestar_Um_Livro_A_Um_Aluno_Inexistente()
    {
      var responseStudent = new Response<Models.Student>();
      responseStudent.Error = new ErrorObjectNotFound("Estudante");

      _studentService.Setup(studentService => studentService.Get(0)).Returns(responseStudent);

      var response = _service.Add(0, 1);

      response.Error.Message.Should().Be("Estudante não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Emprestar_Um_Livro_Inexistente_A_Um_Aluno()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var responseStudent = new Response<Models.Student>();
      responseStudent.Result = student;

      var responseBook = new Response<Models.Book>();
      responseBook.Error = new ErrorObjectNotFound("Livro");

      _studentService.Setup(studentService => studentService.Get(1)).Returns(responseStudent);
      _bookService.Setup(bookService => bookService.GetWithLoans(0)).Returns(responseBook);

      var response = _service.Add(1, 0);

      response.Error.Message.Should().Be("Livro não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Emprestar_Um_Livro_Indisponivel_A_Um_Aluno()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var responseBook = new Response<Models.Book>();
      responseBook.Result = book;

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var responseStudent = new Response<Models.Student>();
      responseStudent.Result = student;

      var loan = new Models.Loan(student, book) { Id = 1 };
      book.SetLoan(loan);

      _studentService.Setup(studentService => studentService.Get(1)).Returns(responseStudent);
      _bookService.Setup(bookService => bookService.GetWithLoans(2)).Returns(responseBook);

      var response = _service.Add(1, 2);

      response.Error.Message.Should().Be("Livro indisponível para empréstimo!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(Error));
    }

    [Fact]
    public void Deve_Ser_Possivel_Registrar_A_Devolucao_De_Um_Livro()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      var loan = new Models.Loan(student, book) { Id = 1 };

      _loans.Setup(repository => repository.Get(It.IsAny<int>())).Returns(loan);

      var response = _service.RegisterBookReturn(1);

      response.Error.Should().BeNull();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Emprestimo_Inexistente_Por_Id()
    {
      var response = _service.RegisterBookReturn(1);

      response.Error.Message.Should().Be("Empréstimo não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Registrar_A_Devolucao_De_Um_Livro_Que_Ja_Foi_Devolvido()
    {
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      var loan = new Models.Loan(student, book) { Id = 1 };
      loan.RegisterReturn();

      _loans.Setup(repository => repository.Get(It.IsAny<int>())).Returns(loan);

      var response = _service.RegisterBookReturn(1);

      response.Error.Message.Should().Be("Já foi registrado a devolução para este empréstimo!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(Error));
    }
  }
}