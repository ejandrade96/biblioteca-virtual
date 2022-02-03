using System.Collections.Generic;

namespace webapp.ViewModels.Home
{
  public class DataSetChartBaseModelViewModel
  {
    public string Label { get; set; } = "Quantidade";

    public bool Fill { get; set; } = true;

    public string BorderColor { get; set; } = "#1f8ef1";

    public int BorderWidth { get; set; } = 2;

    public double BorderDashOffset { get; set; } = 0.0;

    public List<int> Data { get; set; }
  }
}