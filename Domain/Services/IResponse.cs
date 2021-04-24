using Domain.Errors;

namespace Domain.Services
{
  public interface IResponse<T>
  {
    T Result { get; set; }

    IError Error { get; set; }

    bool HasError();
  }
}