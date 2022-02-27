using System;
using System.Globalization;

namespace Infrastructure.Helpers
{
  public static class DateHelper
  {
    public static DateTime StartOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);

    public static DateTime EndOfDay(this DateTime date) => new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);

    public static DateTime FirstDayOfMonth(this DateTime date) => new DateTime(date.Year, date.Month, 01, 0, 0, 0, 0);

    public static string AbbreviatedNameDay(this DateTime date) => date.ToString("ddd", new CultureInfo("pt-BR"));

    public static string AbbreviatedNameMonth(int month) => new DateTime(1990, month, 1).ToString("MMM", new CultureInfo("pt-BR"));
  }
}