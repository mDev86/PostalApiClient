using System.Text.Json;
using System.Text.RegularExpressions;

namespace PostalApiClient.Utilities;

internal class CustomSnakeCseLowerPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        var nameSplit = Regex.Split(name, "(?<!^)(?=[A-Z])");
        
        if (nameSplit.Length > 1)
        {
            name = string.Join("_", nameSplit);
        }
        else if (name.Equals("Expansions"))
        {
            name = "_" + name;
        }

        name = name.ToLower();

        return name;
    }
}