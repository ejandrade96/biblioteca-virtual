using Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Tests.Unit.ViewModels.Home
{
  public class IndexViewModel
  {
    private readonly Mock<IStudent> _studentService;

    private readonly Mock<IBook> _bookService;

    private readonly Mock<ILoan> _loanService;

    private readonly webapp.ViewModels.Home.IndexViewModel _viewModel;

    public IndexViewModel()
    {
      _studentService = new Mock<IStudent>();
      _bookService = new Mock<IBook>();
      _loanService = new Mock<ILoan>();
      _viewModel = new webapp.ViewModels.Home.IndexViewModel(_studentService.Object, _bookService.Object, _loanService.Object);
    }

    [Fact]
    public void Deve_Retornar_Uma_Lista_De_Labels_Referente_Aos_Dias_De_Um_Periodo_Onde_Cada_Label_Representara_As_Tres_Letras_Iniciais_De_Um_Dia_Em_Caixa_Alta()
    {
      var period = 5;
      var day1 = ToLabelFormat(DateTime.Now.AddDays(-(period - 1)));
      var day2 = ToLabelFormat(DateTime.Now.AddDays(-(period - 2)));
      var day3 = ToLabelFormat(DateTime.Now.AddDays(-(period - 3)));
      var day4 = ToLabelFormat(DateTime.Now.AddDays(-(period - 4)));
      var day5 = ToLabelFormat(DateTime.Now);
      var expectedLabels = new List<string> { day1, day2, day3, day4, day5 };

      var result = _viewModel.GetPeriodLabels(period);

      result.SequenceEqual(expectedLabels).Should().BeTrue();
    }

    private string ToLabelFormat(DateTime date) => date.ToString("ddd", new CultureInfo("pt-BR")).ToUpper().Replace(".", "");
  }
}
