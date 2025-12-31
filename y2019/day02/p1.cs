var inputFile = args[0];
var vm = new VM(File.ReadLines(inputFile).First());
var exitCode = vm.Run();
System.Console.WriteLine($"Execution ended: {exitCode}");

enum OpCodes
{
    Add = 1,
    Multiply = 2,
    Halt = 99,
}

class VM
{
    private List<int> _ram;
    private int _pointer = 0;
    private bool _running = false;

    public VM(string rom)
    {
        _ram = [];
        foreach (var element in rom.Split(","))
        {
            _ram.Add(int.Parse(element.Trim()));
        }

        // Set "1202 program alarm" state
        _ram[1] = 12;
        _ram[2] = 2;
    }

    public int Run()
    {
        _running = true;
        while (_running)
        {
            var instruction = (OpCodes)_ram[_pointer];

            switch (instruction)
            {
                case OpCodes.Halt:
                    _running = false;
                    break;
                case OpCodes.Add:
                    Add();
                    break;
                case OpCodes.Multiply:
                    Multiply();
                    break;
                default:
                    System.Console.WriteLine(instruction);
                    _pointer++;
                    break;
            }
        }
        return _ram[0];
    }

    private int Read(int address)
    {
        return _ram[address];
    }

    private void Write(int address, int value)
    {
        _ram[address] = value;
    }

    private void Add()
    {
        var a = Read(Read(_pointer + 1));
        var b = Read(Read(_pointer + 2));
        var c = _ram[_pointer + 3];
        Write(c, a + b);
        _pointer += 4;
        System.Console.WriteLine($"Add {a} + {b} = {a + b} => {c}");
    }

    private void Multiply()
    {
        var a = Read(Read(_pointer + 1));
        var b = Read(Read(_pointer + 2));
        var c = _ram[_pointer + 3];
        Write(c, a * b);
        _pointer += 4;
        System.Console.WriteLine($"Multiply {a} * {b} = {a + b} => {c}");
    }
}
