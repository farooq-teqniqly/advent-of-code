using AdventOfCode.Project1.Types;

namespace AdventOfCode.Project1.Tests;

public class ProgramTestsFixture
{
    public IEnumerable<string> Lines { get; }
    public IEnumerable<Group> Groups { get; }
    public IEnumerable<GroupTotal> GroupTotals { get; }
    
    public ProgramTestsFixture()
    {
        const string input = "100\n\n200\n300\n400\n";
        TextReader reader = new StringReader(input);
        Lines = Functions.ReadLines(reader).ToArrayAsync().Result;
        Groups = Functions.CreateGroups(Lines).ToArray();
        GroupTotals = Functions.GetGroupTotals(Groups).ToArray();
    }
}