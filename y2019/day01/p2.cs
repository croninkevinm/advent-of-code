var inputFile = args[0];
var lines = File.ReadLines(inputFile);

var totalFuel = 0;

foreach (var line in lines)
{
    var mass = int.Parse(line);
    totalFuel += CalculateFuelNeeded(mass);
}
System.Console.WriteLine(totalFuel);

int CalculateFuelNeeded(int mass)
{
    var fuel = (int)Math.Floor(mass / 3d) - 2;
    System.Console.WriteLine($"Mass: {mass}, Fuel {fuel}");
    if (fuel <= 0)
        return 0;
    return fuel + CalculateFuelNeeded(fuel);
}
