using AdventOfCode.Project;

namespace AdventOfCode.Project1.Tests;

public class ProgramTestsFixture
{
    public IEnumerable<string> Lines { get; }
    
    public ProgramTestsFixture()
    {
        const string input = "100\n\n200\n300\n400\n\n50\n60";
        TextReader reader = new StringReader(input);
        Lines = Functions.ReadLines(reader).ToArrayAsync().Result;
    }
}