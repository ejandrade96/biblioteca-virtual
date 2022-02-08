using System.Collections.Generic;

namespace Domain.Services
{
  public interface IGroupingResponse<TKey, TElement>
  {
    TKey Key { get; set; }

    IEnumerable<TElement> Elements { get; set; }
  }
}