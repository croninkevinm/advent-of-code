using System.Threading.Tasks.Dataflow;

var inputFile = args[0];
var vm = new VM(File.ReadLines(inputFile).First());

for (int noun = 0; noun < 100; noun++)
{
    for (int verb = 0; verb < 100; verb++)
    {
        vm.Reset();
        var exitCode = vm.Run(noun, verb);
        System.Console.WriteLine($"Execution ended: {exitCode}");
        if (exitCode == 19690720)
        {
            System.Console.WriteLine(
                $"19690720 Found! Noun:{noun} Verb:{verb} Answer:{100 * noun + verb}"
            );
            return;
        }
    }
}

enum Opcode
{
    Add = 1,
    Multiply = 2,
    Halt = 99,
}

enum LogLevel
{
    Verbose = -1,
    Trace = 0,
    Debug = 1,
    Message = 2,
    Warning = 3,
    Error = 4,
    Fatial = 5,
}

class VM
{
    private LogLevel _logLevel;
    private readonly List<int> _rom;
    private List<int> _memory;
    private int _pointer = 0;
    private bool _running = false;

    public VM(string rom, LogLevel logLevel = LogLevel.Message)
    {
        _logLevel = logLevel;
        _rom = [];
        _memory = [];
        foreach (var element in rom.Split(","))
        {
            _rom.Add(int.Parse(element.Trim()));
        }
        Reset();
    }

    public void Reset()
    {
        _memory = new(_rom);
        _pointer = 0;
    }

    public int Run(int? noun = null, int? verb = null)
    {
        if (noun != null)
            _memory[1] = (int)noun;
        if (verb != null)
            _memory[2] = (int)verb;

        _running = true;
        while (_running)
        {
            var opcode = (Opcode)Read(_pointer);
            switch (opcode)
            {
                case Opcode.Halt:
                    _running = false;
                    break;
                case Opcode.Add:
                    Add();
                    break;
                case Opcode.Multiply:
                    Multiply();
                    break;
                default:
                    Log(LogLevel.Warning, $"Unknown opcpde: {opcode}");
                    _pointer++;
                    break;
            }
        }
        return _memory[0];
    }

    private void Log(LogLevel level, string message)
    {
        if (level >= _logLevel)
        {
            System.Console.WriteLine($"{level}: {message}");
        }
    }

    private int Read(int address)
    {
        var value = _memory[address];
        Log(LogLevel.Trace, $"Reading memory @{address} => {value}");
        return value;
    }

    private void Write(int address, int value)
    {
        Log(LogLevel.Trace, $"Writing memory {value} => @{address}");
        _memory[address] = value;
    }

    private void Add()
    {
        var a = Read(Read(_pointer + 1));
        var b = Read(Read(_pointer + 2));
        var c = _memory[_pointer + 3];
        Write(c, a + b);
        _pointer += 4;
        Log(LogLevel.Debug, $"Add {a} + {b} = {a + b} => {c}");
    }

    private void Multiply()
    {
        var a = Read(Read(_pointer + 1));
        var b = Read(Read(_pointer + 2));
        var c = _memory[_pointer + 3];
        Write(c, a * b);
        _pointer += 4;
        Log(LogLevel.Debug, $"Multiply {a} * {b} = {a + b} => {c}");
    }
}
