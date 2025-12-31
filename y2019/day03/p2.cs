var inputFile = args[0];
var lines = File.ReadLines(inputFile);

var wire2 = lines.ElementAt(1).Split(",");

var points = new Dictionary<(int, int), Point>();

TraceWire(lines.ElementAt(0).Split(","), 1, points);
TraceWire(lines.ElementAt(1).Split(","), 2, points);

var intersectionDistance = int.MaxValue;
foreach (var point in points.Values)
{
    // System.Console.WriteLine($"{point.Key.Item1},{point.Key.Item2} {point.Value.Count}");

    if (!point.Wires.Contains(1) || !point.Wires.Contains(2))
        continue;

    intersectionDistance = Math.Min(intersectionDistance, point.Steps);
}

System.Console.WriteLine(intersectionDistance);

void TraceWire(IEnumerable<string> moves, int id, Dictionary<(int, int), Point> points)
{
    var x = 0;
    var y = 0;
    var steps = 0;
    foreach (var move in moves)
    {
        var direction = move.Substring(0, 1);
        var distance = int.Parse(move.Substring(1));
        var dX = 0;
        var dY = 0;
        switch (direction)
        {
            case "R":
                dX = 1;
                dY = 0;
                break;
            case "L":
                dX = -1;
                dY = 0;
                break;
            case "U":
                dX = 0;
                dY = 1;
                break;
            case "D":
                dX = 0;
                dY = -1;
                break;
        }

        for (int i = 0; i < distance; i++)
        {
            x += dX;
            y += dY;

            steps += Math.Abs(dX) + Math.Abs(dY);

            if (!points.ContainsKey((x, y)))
                points[(x, y)] = new();
            if (points[(x, y)].Wires.Contains(id))
                continue;

            points[(x, y)].Wires.Add(id);
            points[(x, y)].Steps += steps;
        }
    }
}

class Point()
{
    public List<int> Wires = new();
    public int Steps;
}
