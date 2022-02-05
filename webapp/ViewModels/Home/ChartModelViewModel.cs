using System.Collections.Generic;

namespace webapp.ViewModels.Home
{
  public class ChartModelViewModel : ChartBaseModelViewModel
  {
    public List<DataSetChartBaseModelViewModel> DataSets { get; set; }
  }
}