using Domain.Services;
using FluentAssertions;
using Infrastructure.Helpers;
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

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, period);

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

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, period);

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
    public void Deve_Reformular_Um_Agrupamento_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteTres()
    {
      var period = 5;
      var day1 = DateTime.Now.AddDays(-(period - 1));
      var day2 = DateTime.Now.AddDays(-(period - 2));
      var day3 = DateTime.Now.AddDays(-(period - 3));
      var day4 = DateTime.Now.AddDays(-(period - 4));
      var day5 = DateTime.Now;
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<DateTime, Models.Book>>
      {
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day1.Date,
          Elements = new List<Models.Book> { book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, period);

      newGrouping.Should().HaveCount(period);
      newGrouping.ElementAt(0).Key.Should().Be(day1.Date);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(1).Key.Should().Be(day2.Date);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.Should().Be(day3.Date);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.Should().Be(day4.Date);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.Should().Be(day5.Date);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteQuatro()
    {
      var period = 5;
      var day1 = DateTime.Now.AddDays(-(period - 1));
      var day2 = DateTime.Now.AddDays(-(period - 2));
      var day3 = DateTime.Now.AddDays(-(period - 3));
      var day4 = DateTime.Now.AddDays(-(period - 4));
      var day5 = DateTime.Now;
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<DateTime, Models.Book>>
      {
        new GroupingResponse<DateTime, Models.Book>
        {
          Key = day5.Date,
          Elements = new List<Models.Book> { book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, period);

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
      newGrouping.ElementAt(4).Elements.Count().Should().Be(1);
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

      var newGrouping = _viewModel.GetGroupingZeroedRecords<Models.Book>(period);

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

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Agrupado_Por_Mes_E_Ano_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteUm()
    {
      var periodInMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Book>>
      {
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = firstDayOfFirstMonth.Month, KeyTwo = firstDayOfFirstMonth.Year },
          Elements = new List<Models.Book> { book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = daySixthMonth.Month, KeyTwo = daySixthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayEleventhMonth.Month, KeyTwo = dayEleventhMonth.Year },
          Elements = new List<Models.Book> { book, book, book, book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, periodInMonths);

      newGrouping.Should().HaveCount(periodInMonths);
      newGrouping.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      newGrouping.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      newGrouping.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      newGrouping.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      newGrouping.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      newGrouping.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      newGrouping.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      newGrouping.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      newGrouping.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      newGrouping.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      newGrouping.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      newGrouping.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      newGrouping.ElementAt(10).Elements.Count().Should().Be(4);
      newGrouping.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      newGrouping.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      newGrouping.ElementAt(11).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Agrupado_Por_Mes_E_Ano_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteDois()
    {
      var periodInMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Book>>
      {
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = daySecondMonth.Month, KeyTwo = daySecondMonth.Year },
          Elements = new List<Models.Book> { book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayThirdMonth.Month, KeyTwo = dayThirdMonth.Year },
          Elements = new List<Models.Book> { book, book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayFourthMonth.Month, KeyTwo = dayFourthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayFifthMonth.Month, KeyTwo = dayFifthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = daySeventhMonth.Month, KeyTwo = daySeventhMonth.Year },
          Elements = new List<Models.Book> { book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayEighthMonth.Month, KeyTwo = dayEighthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayNinthMonth.Month, KeyTwo = dayNinthMonth.Year },
          Elements = new List<Models.Book> { book, book, book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayTenthMonth.Month, KeyTwo = dayTenthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        },
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayEleventhMonth.Month, KeyTwo = dayEleventhMonth.Year },
          Elements = new List<Models.Book> { book }
        },
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, periodInMonths);

      newGrouping.Should().HaveCount(periodInMonths);
      newGrouping.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      newGrouping.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      newGrouping.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      newGrouping.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(3);
      newGrouping.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      newGrouping.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      newGrouping.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      newGrouping.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      newGrouping.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      newGrouping.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      newGrouping.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(4);
      newGrouping.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      newGrouping.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(2);
      newGrouping.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      newGrouping.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      newGrouping.ElementAt(10).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      newGrouping.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      newGrouping.ElementAt(11).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Agrupado_Por_Mes_E_Ano_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteTres()
    {
      var periodInMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Book>>
      {
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = firstDayOfFirstMonth.Month, KeyTwo = firstDayOfFirstMonth.Year },
          Elements = new List<Models.Book> { book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, periodInMonths);

      newGrouping.Should().HaveCount(periodInMonths);
      newGrouping.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      newGrouping.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(1);
      newGrouping.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      newGrouping.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      newGrouping.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      newGrouping.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      newGrouping.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      newGrouping.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      newGrouping.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      newGrouping.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      newGrouping.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      newGrouping.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      newGrouping.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      newGrouping.ElementAt(10).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      newGrouping.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      newGrouping.ElementAt(11).Elements.Count().Should().Be(0);
    }

    [Fact]
    public void Deve_Reformular_Um_Agrupamento_Agrupado_Por_Mes_E_Ano_Mantendo_A_Quantidade_De_Grupos_Igual_A_Quantidade_De_Dias_Em_Um_Periodo_ParteQuatro()
    {
      var periodInMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);
      var book = new Models.Book("Clean Code", "Robert C. Martin", "8576082675", 431, 1);
      var grouping = new List<IGroupingResponse<IGroupingResponseKey<int, int>, Models.Book>>
      {
        new GroupingResponse<IGroupingResponseKey<int, int>, Models.Book>
        {
          Key = new GroupingResponseKey<int, int> { KeyOne = dayTwelfthMonth.Month, KeyTwo = dayTwelfthMonth.Year },
          Elements = new List<Models.Book> { book, book }
        }
      };

      var newGrouping = _viewModel.ReshapeGrouping<Models.Book>(grouping, periodInMonths);

      newGrouping.Should().HaveCount(periodInMonths);
      newGrouping.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      newGrouping.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      newGrouping.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      newGrouping.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      newGrouping.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      newGrouping.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      newGrouping.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      newGrouping.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      newGrouping.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      newGrouping.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      newGrouping.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      newGrouping.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      newGrouping.ElementAt(10).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      newGrouping.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      newGrouping.ElementAt(11).Elements.Count().Should().Be(2);
    }

    [Fact]
    public void Deve_Retornar_Um_Agrupamento_Agrupado_Por_Mes_E_Ano_Com_Os_Registros_Zerados_Onde_A_Quantidade_De_Grupos_Sera_Igual_A_Quantidade_De_Dias_De_Um_Periodo()
    {
      var periodInMonths = 12;
      var firstDayOfFirstMonth = DateTime.Now.AddMonths(-11).FirstDayOfMonth();
      var daySecondMonth = firstDayOfFirstMonth.AddMonths(1);
      var dayThirdMonth = daySecondMonth.AddMonths(1);
      var dayFourthMonth = dayThirdMonth.AddMonths(1);
      var dayFifthMonth = dayFourthMonth.AddMonths(1);
      var daySixthMonth = dayFifthMonth.AddMonths(1);
      var daySeventhMonth = daySixthMonth.AddMonths(1);
      var dayEighthMonth = daySeventhMonth.AddMonths(1);
      var dayNinthMonth = dayEighthMonth.AddMonths(1);
      var dayTenthMonth = dayNinthMonth.AddMonths(1);
      var dayEleventhMonth = dayTenthMonth.AddMonths(1);
      var dayTwelfthMonth = dayEleventhMonth.AddMonths(1);

      var newGrouping = _viewModel.GetGroupingDoubleKeyWithZeroedRecords<Models.Book>(periodInMonths);

      newGrouping.Should().HaveCount(periodInMonths);
      newGrouping.ElementAt(0).Key.KeyOne.Should().Be(firstDayOfFirstMonth.Month);
      newGrouping.ElementAt(0).Key.KeyTwo.Should().Be(firstDayOfFirstMonth.Year);
      newGrouping.ElementAt(0).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(1).Key.KeyOne.Should().Be(daySecondMonth.Month);
      newGrouping.ElementAt(1).Key.KeyTwo.Should().Be(daySecondMonth.Year);
      newGrouping.ElementAt(1).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(2).Key.KeyOne.Should().Be(dayThirdMonth.Month);
      newGrouping.ElementAt(2).Key.KeyTwo.Should().Be(dayThirdMonth.Year);
      newGrouping.ElementAt(2).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(3).Key.KeyOne.Should().Be(dayFourthMonth.Month);
      newGrouping.ElementAt(3).Key.KeyTwo.Should().Be(dayFourthMonth.Year);
      newGrouping.ElementAt(3).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(4).Key.KeyOne.Should().Be(dayFifthMonth.Month);
      newGrouping.ElementAt(4).Key.KeyTwo.Should().Be(dayFifthMonth.Year);
      newGrouping.ElementAt(4).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(5).Key.KeyOne.Should().Be(daySixthMonth.Month);
      newGrouping.ElementAt(5).Key.KeyTwo.Should().Be(daySixthMonth.Year);
      newGrouping.ElementAt(5).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(6).Key.KeyOne.Should().Be(daySeventhMonth.Month);
      newGrouping.ElementAt(6).Key.KeyTwo.Should().Be(daySeventhMonth.Year);
      newGrouping.ElementAt(6).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(7).Key.KeyOne.Should().Be(dayEighthMonth.Month);
      newGrouping.ElementAt(7).Key.KeyTwo.Should().Be(dayEighthMonth.Year);
      newGrouping.ElementAt(7).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(8).Key.KeyOne.Should().Be(dayNinthMonth.Month);
      newGrouping.ElementAt(8).Key.KeyTwo.Should().Be(dayNinthMonth.Year);
      newGrouping.ElementAt(8).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(9).Key.KeyOne.Should().Be(dayTenthMonth.Month);
      newGrouping.ElementAt(9).Key.KeyTwo.Should().Be(dayTenthMonth.Year);
      newGrouping.ElementAt(9).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(10).Key.KeyOne.Should().Be(dayEleventhMonth.Month);
      newGrouping.ElementAt(10).Key.KeyTwo.Should().Be(dayEleventhMonth.Year);
      newGrouping.ElementAt(10).Elements.Count().Should().Be(0);
      newGrouping.ElementAt(11).Key.KeyOne.Should().Be(dayTwelfthMonth.Month);
      newGrouping.ElementAt(11).Key.KeyTwo.Should().Be(dayTwelfthMonth.Year);
      newGrouping.ElementAt(11).Elements.Count().Should().Be(0);
    }
  }
}
