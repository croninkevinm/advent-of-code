var inputFile = args[0];
var lines = File.ReadLines(inputFile);

var zeros = 0;
var dial = 50;

foreach (var line in lines)
{
    var direction = line.Substring(0, 1);
    var clicks = int.Parse(line.Substring(1));

    var multiplier = direction switch
    {
        "L" => -1,
        "R" => 1,
        _ => 0,
    };

    dial = Mod(dial + (clicks * multiplier));

    zeros += dial == 0 ? 1 : 0;
}

System.Console.WriteLine(zeros);

int Mod(int number, int divisor = 100)
{
    return (number % divisor + divisor) % divisor;
}
