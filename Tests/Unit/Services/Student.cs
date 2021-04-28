using System.Collections.Generic;
using System.Linq;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Errors;
using Moq;
using Xunit;
using Models = Domain.Models;
using Service = Services;

namespace Tests.Unit.Services
{
  public class Student
  {
    private readonly IStudent _service;

    private readonly Mock<IStudents> _students;

    public Student()
    {
      _students = new Mock<IStudents>();
      _service = new Service.Student(_students.Object);
    }

    [Fact]
    public void Deve_Cadastrar_Um_Aluno_Quando_Enviar_Dados_Certos()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);

      _students.Setup(repository => repository.Add(It.IsAny<Models.Student>()))
        .Returns(new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 });

      var response = _service.Add(student);

      response.Error.Should().BeNull();
      response.Result.Id.Should().BeGreaterThan(0);
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Aluno_Com_Email_Existente()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);

      _students.Setup(repository => repository.First(x => x.Contact.Email == student.Contact.Email)).Returns(student);

      var response = _service.Add(student);

      response.Error.Message.Should().Be("Estudante já cadastrado(a) com este email!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorExistingObject));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Cadastrar_Um_Aluno_Com_Login_Existente()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);

      _students.Setup(repository => repository.First(x => x.Login == student.Login)).Returns(student);

      var response = _service.Add(student);

      response.Error.Message.Should().Be("Estudante já cadastrado(a) com este login!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorExistingObject));
    }

    [Fact]
    public void Deve_Listar_Todos_Alunos()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = new StreetType("R");
      var state = new State("SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var students = new List<Models.Student> { student, student };

      _students.Setup(repository => repository.GetAll()).Returns(students.AsQueryable());

      var studentsFound = _service.GetAll();

      studentsFound.Should().HaveCountGreaterThan(0);
      studentsFound.ToList().ForEach(student =>
      {
        student.Id.Should().NotBe(null);
        student.Id.Should().BeGreaterThan(0);
        student.Name.Should().NotBeNullOrWhiteSpace();
        student.Login.Should().NotBeNullOrWhiteSpace();
        student.Record.Should().NotBe(null);
        student.Record.Should().BeGreaterThan(0);
        student.Contact.Email.Should().NotBeNullOrWhiteSpace();
        student.Contact.CellPhone.Should().NotBeNullOrWhiteSpace();
        student.Contact.Telephone.Should().NotBeNullOrWhiteSpace();
        student.Address.ZipCode.Should().NotBeNullOrWhiteSpace();
        student.Address.StreetType.Code.Should().NotBeNullOrWhiteSpace();
        student.Address.StreetType.Description.Should().NotBeNullOrWhiteSpace();
        student.Address.Street.Should().NotBeNullOrWhiteSpace();
        student.Address.Number.Should().NotBe(null);
        student.Address.Number.Should().BeGreaterThan(0);
        student.Address.District.Should().NotBeNullOrWhiteSpace();
        student.Address.City.Should().NotBeNullOrWhiteSpace();
        student.Address.State.Acronym.Should().NotBeNullOrWhiteSpace();
        student.Address.State.Code.Should().BeGreaterThan(0);
        student.Address.State.Code.Should().NotBe(null);
        student.Address.State.Name.Should().NotBeNullOrWhiteSpace();
        student.Address.Complement.Should().NotBeNullOrWhiteSpace();
      });
    }

    [Fact]
    public void Deve_Atualizar_Um_Aluno_Quando_Enviar_Dados_Certos()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      _students.Setup(repository => repository.Get(It.IsAny<int>())).Returns(student);
      _students.Setup(repository => repository.Update(It.IsAny<Models.Student>()));

      var response = _service.Update(student);

      response.Error.Should().BeNull();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Aluno_Inexistente()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      var response = _service.Update(student);

      response.Error.Message.Should().Be("Estudante não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Aluno_Com_Email_Existente()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var studentToUpdate = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 2 };

      _students.Setup(repository => repository.Get(It.IsAny<int>())).Returns(studentToUpdate);
      _students.Setup(repository => repository.First(x => x.Contact.Email == studentToUpdate.Contact.Email && x.Id != studentToUpdate.Id)).Returns(student);

      var response = _service.Update(studentToUpdate);

      response.Error.Message.Should().Be("Estudante já cadastrado(a) com este email!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorExistingObject));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Atualizar_Um_Aluno_Com_Login_Existente()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var studentToUpdate = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 2 };

      _students.Setup(repository => repository.Get(It.IsAny<int>())).Returns(studentToUpdate);
      _students.Setup(repository => repository.First(x => x.Login == studentToUpdate.Login && x.Id != studentToUpdate.Id)).Returns(student);

      var response = _service.Update(studentToUpdate);

      response.Error.Message.Should().Be("Estudante já cadastrado(a) com este login!");
      response.Error.StatusCode.Should().Be(400);
      response.Error.GetType().Should().Be(typeof(ErrorExistingObject));
    }

    [Fact]
    public void Deve_Deletar_Um_Aluno()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      _students.Setup(repository => repository.Get(1)).Returns(student);

      var response = _service.Remove(1);

      response.Error.Should().BeNull();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Deletar_Um_Aluno_Inexistente()
    {
      var response = _service.Remove(1);

      response.Error.Message.Should().Be("Estudante não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_Um_Aluno_Por_Id()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = new StreetType("R");
      var state = new State("SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };

      _students.Setup(repository => repository.Get(1)).Returns(student);

      var response = _service.Get(1);
      var studentFound = response.Result;

      response.Error.Should().BeNull();
      studentFound.Should().NotBeNull();
      studentFound.Id.Should().NotBe(null);
      studentFound.Id.Should().BeGreaterThan(0);
      studentFound.Name.Should().NotBeNullOrWhiteSpace();
      studentFound.Login.Should().NotBeNullOrWhiteSpace();
      studentFound.Record.Should().NotBe(null);
      studentFound.Record.Should().BeGreaterThan(0);
      studentFound.Contact.Email.Should().NotBeNullOrWhiteSpace();
      studentFound.Contact.CellPhone.Should().NotBeNullOrWhiteSpace();
      studentFound.Contact.Telephone.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.ZipCode.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.StreetType.Code.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.StreetType.Description.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.Street.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.Number.Should().NotBe(null);
      studentFound.Address.Number.Should().BeGreaterThan(0);
      studentFound.Address.District.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.City.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.State.Acronym.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.State.Code.Should().BeGreaterThan(0);
      studentFound.Address.State.Code.Should().NotBe(null);
      studentFound.Address.State.Name.Should().NotBeNullOrWhiteSpace();
      studentFound.Address.Complement.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Tentar_Buscar_Um_Aluno_Inexistente_Por_Id()
    {
      var response = _service.Get(1);

      response.Error.Message.Should().Be("Estudante não encontrado(a)!");
      response.Error.StatusCode.Should().Be(404);
      response.Error.GetType().Should().Be(typeof(ErrorObjectNotFound));
    }

    [Fact]
    public void Deve_Retornar_O_Proximo_Numero_De_Matricula_Do_Aluno()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var students = new List<Models.Student>
      {
        student,
        new Models.Student("José Alves Gomes", "jose.gomes", 125479, contact, address) { Id = 2 }
      };

      _students.Setup(repository => repository.GetAll()).Returns(students.AsQueryable());

      var record = _service.GetNextRecord();

      record.Should().Be(125480);
    }

    [Fact]
    public void Deve_Retornar_O_Numero_De_Matricula_125478_Quando_Nao_Existir_Aluno_Cadastrado()
    {
      var record = _service.GetNextRecord();

      record.Should().Be(125478);
    }
  }
}