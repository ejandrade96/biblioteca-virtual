using Domain.Services;

namespace Services
{
  public class GroupingResponseKey<TKeyOne, TKeyTwo> : IGroupingResponseKey<TKeyOne, TKeyTwo>
  {
    public TKeyOne KeyOne { get; set; }

    public TKeyTwo KeyTwo { get; set; }
  }
}
