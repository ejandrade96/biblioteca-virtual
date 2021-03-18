using Domain.Errors;
using Domain.Models;
using Domain.Services;

namespace Services
{
  public class Response<T> : IResponse<T> where T : EntityBase
  {
    public T Result { get; set; }

    public IError Error { get; set; }

    public bool HasError() => Error != null;
  }
}