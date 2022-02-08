using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Services;

namespace webapp.ViewModels.Home
{
  public class IndexViewModel
  {
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

    public IndexViewModel(IStudent studentService, IBook bookService)
    {
      var groupingNewStudents = studentService.GetNumberStudentsAddedInPeriod(5);
      ChartModelNewStudents = new ChartModelViewModel
      {
        Labels = groupingNewStudents.Select(x => ToLabelFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewStudents.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      var groupingNewBooks = bookService.GetNumberBooksAddedInPeriod(5);
      ChartModelNewBooks = new ChartModelViewModel
      {
        Labels = groupingNewBooks.Select(x => ToLabelFormat(x.Key)).ToList(),
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = groupingNewBooks.Select(x => x.Elements.Count()).ToList()
          }
        }
      };

      ChartModelLoans = new ChartModelViewModel
      {
        Labels = new List<string> { "SEG", "TER", "QUA", "QUI", "SEX" },
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = new List<int> { 145, 120, 110, 147, 100 }
          }
        }
      };

      ChartModelLoanReturns = new ChartModelViewModel
      {
        Labels = new List<string> { "SEG", "TER", "QUA", "QUI", "SEX" },
        DataSets = new List<DataSetChartBaseModelViewModel>
        {
          new DataSetChartBaseModelViewModel
          {
            Data = new List<int> { 21, 27, 35, 47, 24 }
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

    private string ToLabelFormat(DateTime date) => date.ToString("ddd", new CultureInfo("pt-BR")).ToUpper().Replace(".", "");
  }
}