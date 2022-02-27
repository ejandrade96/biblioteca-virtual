namespace Domain.Services
{
  public interface IGroupingResponseKey<TKeyOne, TKeyTwo>
  {
    TKeyOne KeyOne { get; set; }

    TKeyTwo KeyTwo { get; set; }
  }
}
