namespace webapp.ViewModels.Home
{
  public class IndexViewModel
  {
    public string Name { get; set; }

    public IndexViewModel(string name)
    {
      Name = name;
    }
  }
}