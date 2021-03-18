using Domain.Errors;
using Domain.Models;

namespace Domain.Services
{
  public interface IResponse<T> where T : EntityBase
  {
    T Result { get; set; }

    IError Error { get; set; }

    bool HasError();
  }
}