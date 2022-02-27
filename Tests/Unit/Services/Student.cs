using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Repository;
using Domain.Services;
using Domain.ValueObjects;
using FluentAssertions;
using Infrastructure.Errors;
using Infrastructure.Helpers;
using Moq;
using Services;
using Xunit;
using Models = Domain.Models;
using Service = Services;

namespace Tests.Unit.Services
{
  public class Student
  {
    private readonly IStudent _service;

    private readonly Mock<ILog> _logService;

    private readonly Mock<IStudents> _students;

    public Student()
    {
      _students = new Mock<IStudents>();
      _logService = new Mock<ILog>();
      _service = new Service.Student(_students.Object, _logService.Object);
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
      var student2 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 2 };
      student2.Inactivate();
      var students = new List<Models.Student> { student, student2 };

      _students.Setup(repository => repository.GetAll()).Returns(students.AsQueryable());

      var studentsFound = _service.GetAll();

      studentsFound.Should().HaveCount(2);
      studentsFound.ToList().ForEach(student =>
      {
        student.Id.Should().NotBe(null);
        student.Id.Should().BeGreaterThan(0);
        student.Name.Should().NotBeNullOrWhiteSpace();
        student.Login.Should().NotBeNullOrWhiteSpace();
        student.Record.Should().NotBe(null);
        student.Record.Should().BeGreaterThan(0);
        student.CreatedAt.Should().NotBe(DateTime.MinValue);
        student.Status.Should().Should().NotBeNull();
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
    public void Deve_Listar_Apenas_Alunos_Ativos()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = new StreetType("R");
      var state = new State("SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var student2 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 2 };
      student2.Inactivate();
      var students = new List<Models.Student> { student, student2, student2 };

      _students.Setup(repository => repository.GetAll()).Returns(students.AsQueryable());

      var studentsFound = _service.GetAll(Status.Active);

      studentsFound.Should().HaveCount(1);
      studentsFound.ToList().ForEach(student =>
      {
        student.Id.Should().NotBe(null);
        student.Id.Should().BeGreaterThan(0);
        student.Name.Should().NotBeNullOrWhiteSpace();
        student.Login.Should().NotBeNullOrWhiteSpace();
        student.Record.Should().NotBe(null);
        student.Record.Should().BeGreaterThan(0);
        student.CreatedAt.Should().NotBe(DateTime.MinValue);
        student.Status.Should().Should().NotBeNull();
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
    public void Deve_Listar_Apenas_Alunos_Inativos()
    {
      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      contact.SetCellPhone("11996582134");
      var streetType = new StreetType("R");
      var state = new State("SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);
      address.SetComplement("AP Torre 1");
      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 1 };
      var student2 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address) { Id = 2 };
      student2.Inactivate();
      var students = new List<Models.Student> { student, student2, student2 };

      _students.Setup(repository => repository.GetAll()).Returns(students.AsQueryable());

      var studentsFound = _service.GetAll(Status.Inactive);

      studentsFound.Should().HaveCount(2);
      studentsFound.ToList().ForEach(student =>
      {
        student.Id.Should().NotBe(null);
        student.Id.Should().BeGreaterThan(0);
        student.Name.Should().NotBeNullOrWhiteSpace();
        student.Login.Should().NotBeNullOrWhiteSpace();
        student.Record.Should().NotBe(null);
        student.Record.Should().BeGreaterThan(0);
        student.CreatedAt.Should().NotBe(DateTime.MinValue);
        student.Status.Should().Should().NotBeNull();
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
      student.CreatedAt.Should().NotBe(DateTime.MinValue);
      studentFound.Status.ToString().Should().Be("Active");
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

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Da_Quantidade_De_Alunos_Adicionados_Em_Um_Determinado_Periodo_De_Dias()
    {
      var numberOfDays = 5;
      var startDay = DateTime.Now.AddDays(-(numberOfDays - 1)).StartOfDay();
      var endDay = DateTime.Now.EndOfDay();

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);

      var student1 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      var firstDay = DateTime.Now.AddDays(-(numberOfDays - 1)).StartOfDay();
      student1.SetCreatedAt(firstDay);

      var student2 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      var secondDay = DateTime.Now.AddDays(-(numberOfDays - 2));
      student2.SetCreatedAt(secondDay);
      var student3 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student3.SetCreatedAt(secondDay.EndOfDay());

      var student4 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      var lastDay = DateTime.Now.EndOfDay();
      student4.SetCreatedAt(lastDay);

      var students = new List<Models.Student> { student1, student2, student3, student4 };

      _students.Setup(repository => repository.FindAll(x => x.CreatedAt >= startDay && x.CreatedAt <= endDay)).Returns(students.AsQueryable());

      var groupingNewStudents = _service.GetNumberStudentsAddedInPeriod(numberOfDays);

      groupingNewStudents.Should().HaveCount(3);
      groupingNewStudents.ElementAt(0).Key.Should().Be(firstDay.Date);
      groupingNewStudents.ElementAt(0).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(1).Key.Should().Be(secondDay.Date);
      groupingNewStudents.ElementAt(1).Elements.Count().Should().Be(2);
      groupingNewStudents.ElementAt(2).Key.Should().Be(lastDay.Date);
      groupingNewStudents.ElementAt(2).Elements.Count().Should().Be(1);
    }

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Da_Quantidade_De_Alunos_Adicionados_Em_Um_Determinado_Periodo_De_Meses()
    {
      var numberOfMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);

      var contact = new Models.Contact("joao.villar@live.com", "1154218547");
      var streetType = StreetType.StreetTypes.First(x => x.Code == "R");
      var state = State.States.First(x => x.Acronym == "SP");
      var address = new Models.Address("09421700", streetType, "dos Vianas", 412, "Centro", "São Bernardo do Campo", state);

      var student = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student.SetCreatedAt(firstDayOfFirstMonth);
      var student2 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student2.SetCreatedAt(firstDayOfFirstMonth);
      var student3 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student3.SetCreatedAt(daySecondMonth);
      var student4 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student4.SetCreatedAt(dayThirdMonth);
      var student5 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student5.SetCreatedAt(dayThirdMonth);
      var student6 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student6.SetCreatedAt(dayFourthMonth);
      var student7 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student7.SetCreatedAt(dayFifthMonth);
      var student8 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student8.SetCreatedAt(daySixthMonth);
      var student9 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student9.SetCreatedAt(daySixthMonth);
      var student10 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student10.SetCreatedAt(daySixthMonth);
      var student11 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student11.SetCreatedAt(daySeventhMonth);
      var student12 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student12.SetCreatedAt(dayEighthMonth);
      var student13 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student13.SetCreatedAt(dayNinthMonth);
      var student14 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student14.SetCreatedAt(dayTenthMonth);
      var student15 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student15.SetCreatedAt(dayEleventhMonth);
      var student16 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student16.SetCreatedAt(dayEleventhMonth);
      var student17 = new Models.Student("João Villar Ferreira", "joao.ferreira", 125478, contact, address);
      student17.SetCreatedAt(dayTwelfthMonth);

      var students = new List<Models.Student>
      {
        student, student2, student3, student4, student5, student6, student7, student8, student9, student10, student11, student12, student13, student14, student15, student16, student17
      };

      _students.Setup(repository => repository.FindAll(x => x.CreatedAt >= firstDayOfFirstMonth && x.CreatedAt <= DateTime.Now)).Returns(students.AsQueryable());

      var groupingNewStudents = _service.GetNumberStudentsAddedInPeriodOfMonths(numberOfMonths);

      groupingNewStudents.Should().HaveCount(12);
      groupingNewStudents.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      groupingNewStudents.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      groupingNewStudents.ElementAt(0).Elements.Count().Should().Be(2);
      groupingNewStudents.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      groupingNewStudents.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      groupingNewStudents.ElementAt(1).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      groupingNewStudents.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      groupingNewStudents.ElementAt(2).Elements.Count().Should().Be(2);
      groupingNewStudents.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      groupingNewStudents.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      groupingNewStudents.ElementAt(3).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      groupingNewStudents.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      groupingNewStudents.ElementAt(4).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      groupingNewStudents.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      groupingNewStudents.ElementAt(5).Elements.Count().Should().Be(3);
      groupingNewStudents.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      groupingNewStudents.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      groupingNewStudents.ElementAt(6).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      groupingNewStudents.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      groupingNewStudents.ElementAt(7).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      groupingNewStudents.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      groupingNewStudents.ElementAt(8).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      groupingNewStudents.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      groupingNewStudents.ElementAt(9).Elements.Count().Should().Be(1);
      groupingNewStudents.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      groupingNewStudents.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      groupingNewStudents.ElementAt(10).Elements.Count().Should().Be(2);
      groupingNewStudents.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      groupingNewStudents.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      groupingNewStudents.ElementAt(11).Elements.Count().Should().Be(1);
    }
  }
}