using System.Text.Json;
using System.Text.RegularExpressions;

namespace PostalApiClient.Utilities;

internal class CustomSnakeCaseLowerPolicy : JsonNamingPolicy
{
    private static readonly Regex NameSplitRegex = new Regex("(?<!^)(?=[A-Z])", RegexOptions.Compiled);

    public override string ConvertName(string name)
    {
        var nameSplit = NameSplitRegex.Split(name);
        
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