namespace webapp.ViewModels.Home
{
  public class DataSetGeneralChartModelViewModel : DataSetChartBaseModelViewModel
  {
    public string PointBackgroundColor { get; set; } = "#1f8ef1";

    public string PointBorderColor { get; set; } = "rgba(255,255,255,0)";

    public string PointHoverBackgroundColor { get; set; } = "#1f8ef1";

    public int PointBorderWidth { get; set; } = 20;

    public int PointHoverRadius { get; set; } = 4;

    public int PointHoverBorderWidth { get; set; } = 15;

    public int PointRadius { get; set; } = 4;
  }
}