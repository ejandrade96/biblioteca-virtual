using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Models;

namespace Domain.Services
{
  public interface IServiceBase<T> where T : EntityBase
  {
    /// <summary>
    /// Returns all records that match the predicate, grouping them by groupByCond
    /// </summary>
    /// <param name="predicate"></param>
    /// /// <param name="groupByCond"></param>
    IEnumerable<IGroupingResponse<DateTime, T>> GetNumberRecordsAddedInPeriod(Expression<Func<T, bool>> predicate, Func<T, DateTime> groupByCond);
  }
}