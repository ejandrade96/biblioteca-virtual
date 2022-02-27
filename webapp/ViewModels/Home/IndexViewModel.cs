using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Models;
using Domain.Services;
using Infrastructure.Helpers;
using Services;

namespace webapp.ViewModels.Home
{
  public class IndexViewModel
  {
    private const int _periodInDays = 5;

    private const int _periodInMonths = 12;

    private readonly IndexViewModelAction _indexViewModelAction = new IndexViewModelAction();

    public ChartModelViewModel ChartModelNewStudents { get; set; }

    public ChartModelViewModel ChartModelNewBooks { get; set; }

    public ChartModelViewModel ChartModelLoans { get; set; }

    public ChartModelViewModel ChartModelLoanReturns { get; set; }

    public GeneralChartModelViewModel GeneralChartModelOfTheYear { get; set; }

    public GeneralChartModelViewModel GeneralChartModelOfTheYearDataTabNewBooks { get; set; }

    public GeneralChartModelViewModel GeneralChartModelOfTheYearDataTabLoans { get; set; }

    public GeneralChartModelViewModel GeneralChartModelOfTheYearDataTabLoanReturns { get; set; }

    public List<StudentViewModel> FiveStarStudents { get; set; }

    public List<BookViewModel> FiveStarBooks { get; set; }

    public IndexViewModel(IStudent studentService, IBook bookService, ILoan loanService)
    {
      var groupingNewStudents = studentService.GetNumberStudentsAddedInPeriod(_periodInDays);
      if (groupingNewStudents.Count() != _periodInDays)
      {
        groupingNewStudents = _indexViewModelAction.ReshapeGrouping<Domain.Models.Student>(groupingNewStudents);
      }
      ChartModelNewStudents = new ChartModelViewModel
      {
        Labels = groupingNewStudents.Select(x => _indexViewModelAction.ToLabelDayFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewStudents.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewBooks = bookService.GetNumberBooksAddedInPeriod(_periodInDays);
      if (groupingNewBooks.Count() != _periodInDays)
      {
        groupingNewBooks = _indexViewModelAction.ReshapeGrouping<Domain.Models.Book>(groupingNewBooks);
      }
      ChartModelNewBooks = new ChartModelViewModel
      {
        Labels = groupingNewBooks.Select(x => _indexViewModelAction.ToLabelDayFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewBooks.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewLoans = loanService.GetNumberLoansAddedInPeriod(_periodInDays);
      if (groupingNewLoans.Count() != _periodInDays)
      {
        groupingNewLoans = _indexViewModelAction.ReshapeGrouping<Domain.Models.Loan>(groupingNewLoans);
      }
      ChartModelLoans = new ChartModelViewModel
      {
        Labels = groupingNewLoans.Select(x => _indexViewModelAction.ToLabelDayFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewLoans.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewReturns = loanService.GetNumberReturnsRecordedInPeriod(_periodInDays);
      if (groupingNewReturns.Count() != _periodInDays)
      {
        groupingNewReturns = _indexViewModelAction.ReshapeGrouping<Domain.Models.Loan>(groupingNewReturns);
      }
      ChartModelLoanReturns = new ChartModelViewModel
      {
        Labels = groupingNewReturns.Select(x => _indexViewModelAction.ToLabelDayFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewReturns.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewStudentsByMonth = studentService.GetNumberStudentsAddedInPeriodOfMonths(_periodInMonths);
      if (groupingNewStudentsByMonth.Count() != _periodInMonths)
      {
        groupingNewStudentsByMonth = _indexViewModelAction.ReshapeGrouping<Domain.Models.Student>(groupingNewStudentsByMonth);
      }
      GeneralChartModelOfTheYear = new GeneralChartModelViewModel
      {
        Labels = groupingNewStudentsByMonth.Select(x => _indexViewModelAction.ToLabelMonthFormat(x.Key.KeyOne)).ToList(),
        DataSets = new List<DataSetGeneralChartModelViewModel>
        {
          new DataSetGeneralChartModelViewModel
          {
            Data = groupingNewStudentsByMonth.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewBooksByMonth = bookService.GetNumberBooksAddedInPeriodOfMonths(_periodInMonths);
      if (groupingNewBooksByMonth.Count() != _periodInMonths)
      {
        groupingNewBooksByMonth = _indexViewModelAction.ReshapeGrouping<Domain.Models.Book>(groupingNewBooksByMonth);
      }
      GeneralChartModelOfTheYearDataTabNewBooks = new GeneralChartModelViewModel
      {
        Labels = groupingNewBooksByMonth.Select(x => _indexViewModelAction.ToLabelMonthFormat(x.Key.KeyOne)).ToList(),
        DataSets = new List<DataSetGeneralChartModelViewModel>
        {
          new DataSetGeneralChartModelViewModel
          {
            Data = groupingNewBooksByMonth.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      GeneralChartModelOfTheYearDataTabLoans = new GeneralChartModelViewModel
      {
        Labels = new List<string> { "JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ" },
        DataSets = new List<DataSetGeneralChartModelViewModel>
        {
          new DataSetGeneralChartModelViewModel
          {
            Data = new List<int> { 60, 80, 65, 130, 80, 105, 90, 130, 70, 115, 60, 130 }
          }
        }
      };

      GeneralChartModelOfTheYearDataTabLoanReturns = new GeneralChartModelViewModel
      {
        Labels = new List<string> { "JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ" },
        DataSets = new List<DataSetGeneralChartModelViewModel>
        {
          new DataSetGeneralChartModelViewModel
          {
            Data = new List<int> { 130, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 500 }
          }
        }
      };

      FiveStarStudents = new List<StudentViewModel>
      {
        new StudentViewModel
        {
          Record = 125478,
          Name = "Elton John Fernandes de Andrade".ToUpper(),
          Login = "ejandrade"
        },
        new StudentViewModel
        {
          Record = 125478,
          Name = "Elton John Fernandes de Andrade".ToUpper(),
          Login = "ejandrade"
        },
        new StudentViewModel
        {
          Record = 125478,
          Name = "Elton John Fernandes de Andrade".ToUpper(),
          Login = "ejandrade"
        },
        new StudentViewModel
        {
          Record = 125478,
          Name = "Elton John Fernandes de Andrade".ToUpper(),
          Login = "ejandrade"
        },
        new StudentViewModel
        {
          Record = 125478,
          Name = "Elton John Fernandes de Andrade".ToUpper(),
          Login = "ejandrade"
        }
      };

      FiveStarBooks = new List<BookViewModel>
      {
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        },
        new BookViewModel
        {
          Title = "Inteligência emocional: a teoria revolucionária que redefine o que é ser",
          Author = "Daniel Goleman",
          ImagePath = "~/images/book/inteligencia-emocional_uploaded_21-12-21_02-08.jpg"
        }
      };
    }

    public sealed class IndexViewModelAction
    {
      public IEnumerable<IGroupingResponse<DateTime, T>> ReshapeGrouping<T>(IEnumerable<IGroupingResponse<DateTime, T>> grouping, int periodInDays = _periodInDays) where T : EntityBase
      {
        if (grouping.Count() == 0)
          return GetGroupingZeroedRecords<T>();

        var dayCounter = DateTime.Now.AddDays(-(periodInDays - 1));
        var newGrouping = grouping.ToList();

        for (var i = 0; i <= grouping.Count(); i++)
        {
          if (newGrouping[i].Key.Date != dayCounter.Date)
          {
            newGrouping.Add(new GroupingResponse<DateTime, T>
            {
              Key = dayCounter.Date,
              Elements = new List<T> { }
            });

            i--;
          }

          //Quando tiver um agrupamento com apenas 1 item com a 'Key' referente ao primeiro dia, permitir que o fluxo adicione o restante dos dias
          if (newGrouping.Count == 1)
            i--;

          if (dayCounter.Date == DateTime.Now.Date)
            break;

          dayCounter = dayCounter.AddDays(1);
        }

        return newGrouping.OrderBy(x => x.Key);
      }

      public IEnumerable<IGroupingResponse<DateTime, T>> GetGroupingZeroedRecords<T>(int periodInDays = _periodInDays) where T : EntityBase
      {
        var dayCounter = DateTime.Now.AddDays(-(periodInDays - 1));
        var grouping = new List<IGroupingResponse<DateTime, T>>();

        for (var i = 0; i < periodInDays; i++)
        {
          grouping.Add(new GroupingResponse<DateTime, T>
          {
            Key = dayCounter.Date,
            Elements = new List<T> { }
          });

          dayCounter = dayCounter.AddDays(1);
        }

        return grouping.OrderBy(x => x.Key);
      }

      public IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, T>> ReshapeGrouping<T>
          (IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, T>> grouping, int periodInMonths = _periodInMonths) where T : EntityBase
      {
        if (grouping.Count() == 0)
          return GetGroupingDoubleKeyWithZeroedRecords<T>();

        var dateCounter = DateTime.Now.AddMonths(-(periodInMonths - 1)).FirstDayOfMonth();
        var newGrouping = grouping.ToList();

        for (var i = 0; i <= grouping.Count(); i++)
        {
          if (newGrouping[i].Key.KeyOne != dateCounter.Month || newGrouping[i].Key.KeyTwo != dateCounter.Year)
          {
            newGrouping.Add(new GroupingResponse<IGroupingResponseKey<int, int>, T>
            {
              Key = new GroupingResponseKey<int, int> { KeyOne = dateCounter.Month, KeyTwo = dateCounter.Year },
              Elements = new List<T> { }
            });

            i--;
          }

          if (newGrouping.Count == 1)
            i--;

          if (dateCounter.Month == DateTime.Now.Month && dateCounter.Year == DateTime.Now.Year)
            break;

          dateCounter = dateCounter.AddMonths(1);
        }

        return newGrouping.OrderBy(x => x.Key.KeyTwo)
                          .ThenBy(x => x.Key.KeyOne);
      }

      public IEnumerable<IGroupingResponse<IGroupingResponseKey<int, int>, T>> GetGroupingDoubleKeyWithZeroedRecords<T>(int periodInMonths = _periodInMonths) where T : EntityBase
      {
        var dateCounter = DateTime.Now.AddMonths(-(periodInMonths - 1)).FirstDayOfMonth();
        var grouping = new List<IGroupingResponse<IGroupingResponseKey<int, int>, T>>();

        for (var i = 0; i < periodInMonths; i++)
        {
          grouping.Add(new GroupingResponse<IGroupingResponseKey<int, int>, T>
          {
            Key = new GroupingResponseKey<int, int> { KeyOne = dateCounter.Month, KeyTwo = dateCounter.Year },
            Elements = new List<T> { }
          });

          dateCounter = dateCounter.AddMonths(1);
        }

        return grouping;
      }

      public string ToLabelDayFormat(DateTime date) => date.AbbreviatedNameDay().ToUpper().Replace(".", "");

      public string ToLabelMonthFormat(int month) => DateHelper.AbbreviatedNameMonth(month).ToUpper().Replace(".", "");
    }
  }
}