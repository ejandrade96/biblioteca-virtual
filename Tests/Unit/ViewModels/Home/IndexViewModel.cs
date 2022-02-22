using Domain.Services;
using FluentAssertions;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Models = Domain.Models;

namespace Tests.Unit.ViewModels.Home
{
  public class IndexViewModel
  {
    private readonly webapp.ViewModels.Home.IndexViewModel.IndexViewModelAction _viewModel;

    public IndexViewModel()
    {
      _viewModel = new webapp.ViewModels.Home.IndexViewModel.IndexViewModelAction();
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteUm()
    {
      var period = 5;
      var day1 = DateTime.Now.AddDays(-(period - 1));
      var day2 = DateTime.Now.AddDays(-(period - 2));
      var day3 = DateTime.Now.AddDays(-(period - 3));
      var day4 = DateTime.Now.AddDays(-(period - 4));
      var day5 = DateTime.Now;
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var book2 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var book3 = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<DateTime, Models.Book>>
      {
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day1.Date,
          Elements = new List<Models.Book> { book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day3.Date,
          Elements = new List<Models.Book> { book2 }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day4.Date,
          Elements = new List<Models.Book> { book3 }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<DateTime, Models.Book>(grouping, period);

      newGrouping.Should().HaveCount(period);
      newGrouping.ElementAt(0).Key.Should().Be(day1.Date);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(1).Key.Should().Be(day2.Date);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.Should().Be(day3.Date);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(3).Key.Should().Be(day4.Date);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(4).Key.Should().Be(day5.Date);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteDois()
    {
      var period = 10;
      var day1 = DateTime.Now.AddDays(-(period - 1));
      var day2 = DateTime.Now.AddDays(-(period - 2));
      var day3 = DateTime.Now.AddDays(-(period - 3));
      var day4 = DateTime.Now.AddDays(-(period - 4));
      var day5 = DateTime.Now.AddDays(-(period - 5));
      var day6 = DateTime.Now.AddDays(-(period - 6));
      var day7 = DateTime.Now.AddDays(-(period - 7));
      var day8 = DateTime.Now.AddDays(-(period - 8));
      var day9 = DateTime.Now.AddDays(-(period - 9));
      var day10 = DateTime.Now;
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<DateTime, Models.Book>>
      {
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day2.Date,
          Elements = new List<Models.Book> { book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day3.Date,
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day4.Date,
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day6.Date,
          Elements = new List<Models.Book> { book, book, book, book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day7.Date,
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day9.Date,
          Elements = new List<Models.Book> { book, book, book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<DateTime, Models.Book>(grouping, period);

      newGrouping.Should().HaveCount(period);
      newGrouping.ElementAt(0).Key.Should().Be(day1.Date);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(1).Key.Should().Be(day2.Date);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(2).Key.Should().Be(day3.Date);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(3).Key.Should().Be(day4.Date);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(4).Key.Should().Be(day5.Date);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(5).Key.Should().Be(day6.Date);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(4);
      newGrouping.ElementAt(6).Key.Should().Be(day7.Date);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(7).Key.Should().Be(day8.Date);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(8).Key.Should().Be(day9.Date);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(3);
      newGrouping.ElementAt(9).Key.Should().Be(day10.Date);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Com_Os_Registros_Zerados_Onde_A_Quantidade_De_Grupos_Sera_Igual_A_Quantidade_De_Dias_De_Um_Periodo()
    {
      var period = 5;
      var day1 = DateTime.Now.AddDays(-(period - 1));
      var day2 = DateTime.Now.AddDays(-(period - 2));
      var day3 = DateTime.Now.AddDays(-(period - 3));
      var day4 = DateTime.Now.AddDays(-(period - 4));
      var day5 = DateTime.Now;

      var newGrouping = _viewModel.GetGroupingZeroedRecords<DateTime, Models.Book>(period);

      newGrouping.Should().HaveCount(period);
      newGrouping.ElementAt(0).Key.Should().Be(day1.Date);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(1).Key.Should().Be(day2.Date);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.Should().Be(day3.Date);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.Should().Be(day4.Date);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.Should().Be(day5.Date);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
    }
  }
}
