var inputFile = args[0];
var range = File.ReadLines(inputFile).First().Split("-");

var min = int.Parse(range[0]);
var max = int.Parse(range[1]);

var valitCount = 0;

for (int i = min; i < max + 1; i++)
{
    var valid = CriteriaMatch(i.ToString());
    System.Console.WriteLine($"Checking: {i}, Valid: {valid}");
    if (valid)
    {
        valitCount++;
    }
}

System.Console.WriteLine(valitCount);

bool CriteriaMatch(string password)
{
    if (password.Length > 6)
        return false;

    for (int i = 0; i < password.Length - 1; i++)
    {
        if (int.Parse(password.Substring(i, 1)) > int.Parse(password.Substring(i + 1, 1)))
            return false;
    }

    for (int i = 0; i < password.Length - 1; i++)
    {
        if (password.Substring(i, 1) == password.Substring(i + 1, 1))
            return true;
    }
    return false;
}
