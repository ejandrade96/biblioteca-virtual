using Domain.Errors;
using Domain.Services;

namespace Services
{
  public class Response<T> : IResponse<T>
  {
    public T Result { get; set; }

    public IError Error { get; set; }

    public bool HasError() => Error != null;
  }
}