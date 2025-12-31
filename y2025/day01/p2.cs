var inputFile = args[0];
var lines = File.ReadLines(inputFile);

var zeros = 0;
var dial = 50;

foreach (var line in lines)
{
    var direction = line.Substring(0, 1);

    var multiplier = direction switch
    {
        "L" => -1,
        "R" => 1,
        _ => 0,
    };
    var clicks = int.Parse(line.Substring(1)) * multiplier;

    zeros += GetZeroCrossings(dial, clicks);

    dial = Mod(dial + clicks);
}

System.Console.WriteLine(zeros);

int Mod(int number, int divisor = 100)
{
    return (number % divisor + divisor) % divisor;
}

int GetZeroCrossings(int dial, int clicks)
{
    int zeros = 0;

    if (clicks > 0) // Rotate right
    {
        if (dial + clicks < 100)
            return 0;

        zeros = clicks / 100;

        if (dial + (clicks % 100) >= 100)
            zeros++;
    }
    else if (clicks < 0) // Rotate Left
    {
        if (dial + clicks > 0)
            return 0;

        zeros = Math.Abs(clicks) / 100;

        if (dial == 0)
            dial = 100;

        if (dial + (clicks % 100) <= 0)
            zeros++;
    }

    return zeros;
}
