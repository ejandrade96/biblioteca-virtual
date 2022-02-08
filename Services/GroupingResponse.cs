using System.Collections.Generic;
using Domain.Services;

namespace Services
{
  public class GroupingResponse<TKey, TElement> : IGroupingResponse<TKey, TElement>
  {
    public TKey Key { get; set; }

    public IEnumerable<TElement> Elements { get; set; }
  }
}