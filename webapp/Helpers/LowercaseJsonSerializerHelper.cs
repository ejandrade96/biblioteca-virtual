using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace webapp.Helpers
{
  public static class LowercaseJsonSerializerHelper
  {
    private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
      ContractResolver = new LowercaseContractResolver()
    };

    public static string SerializeObject(object o)
    {
      return JsonConvert.SerializeObject(o, Formatting.Indented, Settings);
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
      protected override string ResolvePropertyName(string propertyName)
      {
        return propertyName.ToLower() == "datasets" ? propertyName.ToLower() : char.ToLower(propertyName[0]) + propertyName.Substring(1);
      }
    }
  }
}