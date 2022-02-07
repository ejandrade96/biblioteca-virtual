using System;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace Domain.Models
{
  public class Student : EntityBase
  {
    public string Name { get; protected set; }

    public string Login { get; protected set; }

    public int Record { get; protected set; }

    public DateTime CreatedAt { get; protected set; }

    public Contact Contact { get; protected set; }

    public Address Address { get; protected set; }

    public Status Status { get; protected set; }

    public List<Loan> Loans { get; protected set; }

    protected Student()
    {
    }

    public Student(string name, string login, int record, Contact contact, Address address)
    {
      Name = name;
      Login = login;
      Record = record;
      Contact = contact;
      Address = address;
      Status = Status.Active;
      CreatedAt = DateTime.Now;
    }

    public void UpdateValues(Student student)
    {
      Name = student.Name;
      Login = student.Login;
      Record = student.Record;
    }

    public void Inactivate() => Status = Status.Inactive;

    public void Activate() => Status = Status.Active;

    public void SetCreatedAt(DateTime date) => CreatedAt = date;
  }
}