using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Models;
using Domain.Services;
using Services;

namespace webapp.ViewModels.Home
{
  public class IndexViewModel
  {
    private const int _periodInDays = 5;

    private readonly IndexViewModelAction _indexViewModelAction = new IndexViewModelAction();

    public ChartModelViewModel ChartModelNewStudents { get; set; }

    public ChartModelViewModel ChartModelNewBooks { get; set; }

    public ChartModelViewModel ChartModelLoans { get; set; }

    public ChartModelViewModel ChartModelLoanReturns { get; set; }

    public GeneralChartModelViewModel GeneralChartModelOfTheYear { get; set; }

    public List<int> GeneralChartModelOfTheYearDataTabNewBooks { get; set; }

    public List<int> GeneralChartModelOfTheYearDataTabLoans { get; set; }

    public List<int> GeneralChartModelOfTheYearDataTabLoanReturns { get; set; }

    public List<StudentViewModel> FiveStarStudents { get; set; }

    public List<BookViewModel> FiveStarBooks { get; set; }

    public IndexViewModel(IStudent studentService, IBook bookService, ILoan loanService)
    {
      var groupingNewStudents = studentService.GetNumberStudentsAddedInPeriod(_periodInDays);
      if (groupingNewStudents.Count() != _periodInDays)
      {
        groupingNewStudents = _indexViewModelAction.ReshapeGrouping<DateTime, Domain.Models.Student>(groupingNewStudents);
      }
      ChartModelNewStudents = new ChartModelViewModel
      {
        Labels = groupingNewStudents.Select(x => _indexViewModelAction.ToLabelFormat(x.Key)).ToList(),
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
        groupingNewBooks = _indexViewModelAction.ReshapeGrouping<DateTime, Domain.Models.Book>(groupingNewBooks);
      }
      ChartModelNewBooks = new ChartModelViewModel
      {
        Labels = groupingNewBooks.Select(x => _indexViewModelAction.ToLabelFormat(x.Key)).ToList(),
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
        groupingNewLoans = _indexViewModelAction.ReshapeGrouping<DateTime, Domain.Models.Loan>(groupingNewLoans);
      }
      ChartModelLoans = new ChartModelViewModel
      {
        Labels = groupingNewLoans.Select(x => _indexViewModelAction.ToLabelFormat(x.Key)).ToList(),
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
        groupingNewReturns = _indexViewModelAction.ReshapeGrouping<DateTime, Domain.Models.Loan>(groupingNewReturns);
      }
      ChartModelLoanReturns = new ChartModelViewModel
      {
        Labels = groupingNewReturns.Select(x => _indexViewModelAction.ToLabelFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewReturns.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      GeneralChartModelOfTheYear = new GeneralChartModelViewModel
      {
        Labels = new List<string> { "JAN", "FEV", "MAR", "ABR", "MAI", "JUN", "JUL", "AGO", "SET", "OUT", "NOV", "DEZ" },
        DataSets = new List<DataSetGeneralChartModelViewModel>
        {
          new DataSetGeneralChartModelViewModel
          {
            Data = new List<int> { 982, 925, 900, 970, 985, 960, 975, 750, 990, 980, 910, 990 }
          }
        }
      };

      GeneralChartModelOfTheYearDataTabNewBooks = new List<int> { 80, 120, 105, 110, 95, 105, 90, 100, 80, 95, 70, 120 };

      GeneralChartModelOfTheYearDataTabLoans = new List<int> { 60, 80, 65, 130, 80, 105, 90, 130, 70, 115, 60, 130 };

      GeneralChartModelOfTheYearDataTabLoanReturns = new List<int> { 130, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 500 };

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
      public IEnumerable<IGroupingResponse<System.DateTime, T>> ReshapeGrouping<DateTime, T>
       (IEnumerable<IGroupingResponse<System.DateTime, T>> grouping, int periodInDays = _periodInDays) where T : EntityBase
      {
        if (grouping.Count() == 0)
          return GetGroupingZeroedRecords<System.DateTime, T>();

        var dayCounter = System.DateTime.Now.AddDays(-(periodInDays - 1));
        var newGrouping = grouping.ToList();

        for (var i = 0; i <= grouping.Count(); i++)
        {
          if (newGrouping[i].Key.Date != dayCounter.Date)
          {
            newGrouping.Add(new GroupingResponse<System.DateTime, T>
            {
              Key = dayCounter.Date,
              Elements = new List<T> { }
            });

            i--;
          }

          if (dayCounter.Date == System.DateTime.Now.Date)
            break;

          dayCounter = dayCounter.AddDays(1);
        }

        return newGrouping.OrderBy(x => x.Key);
      }

      public IEnumerable<IGroupingResponse<System.DateTime, T>> GetGroupingZeroedRecords<DateTime, T>(int periodInDays = _periodInDays)
      {
        var dayCounter = System.DateTime.Now.AddDays(-(_periodInDays - 1));
        var grouping = new List<IGroupingResponse<System.DateTime, T>>();

        for (var i = 0; i < periodInDays; i++)
        {
          grouping.Add(new GroupingResponse<System.DateTime, T>
          {
            Key = dayCounter.Date,
            Elements = new List<T> { }
          });

          dayCounter = dayCounter.AddDays(1);
        }

        return grouping.OrderBy(x => x.Key);
      }

      public string ToLabelFormat(DateTime date) => date.ToString("ddd", new CultureInfo("pt-BR")).ToUpper().Replace(".", "");
    }
  }
}