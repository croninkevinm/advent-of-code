using System.Globalization;

var inputFile = args[0];
var range = File.ReadLines(inputFile).First().Split("-");

var min = int.Parse(range[0]);
var max = int.Parse(range[1]);

var valitCount = 0;

for (int i = min; i <= max; i++)
{
    var valid = CriteriaMatch(i.ToString());
    if (valid)
    {
        valitCount++;
    }
}

System.Console.WriteLine(valitCount);

bool CriteriaMatch(string password)
{
    if (password.Length != 6)
    {
        // System.Console.WriteLine($"{password}: wrong length");
        return false;
    }

    for (int i = 0; i < password.Length - 1; i++)
    {
        if (int.Parse(password.Substring(i, 1)) > int.Parse(password.Substring(i + 1, 1)))
        {
            // System.Console.WriteLine($"{password}: decreasing pair of digits");
            return false;
        }
    }

    var hasMatchingPair = false;
    for (int i = 0; i < password.Length - 1; i++)
    {
        var matchingSetSize = 1;
        for (int j = i; j < password.Length; j++)
        {
            if (j + 1 < password.Length && password.Substring(j, 1) == password.Substring(j + 1, 1))
            {
                matchingSetSize++;
            }
            else
            {
                // System.Console.WriteLine($"{password}: {password.Substring(j, 1)} SetSize: {matchingSetSize});
                i = j;
                break;
            }
        }
        if (matchingSetSize == 2)
        {
            hasMatchingPair = true;
        }
    }
    if (hasMatchingPair)
    {
        System.Console.WriteLine($"{password}: valid");
        return true;
    }
    return false;
}
