var inputFile = args[0];
var lines = File.ReadLines(inputFile);

var totalFuel = 0;

foreach (var line in lines)
{
    var mass = int.Parse(line);
    var fuel = (int)Math.Floor(mass / 3d) - 2;
    totalFuel += fuel;
    System.Console.WriteLine($"Mass: {mass}, Fuel {fuel}");
}
System.Console.WriteLine(totalFuel);
