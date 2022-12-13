namespace AdventOfCode.Project.Tests;

public class ProgramTestsFixture
{
    public IEnumerable<string> CalorieLineItems { get; }
    public IEnumerable<string> RucksackLineItems { get; }
    public IEnumerable<string> RpsLineItems { get; }
    public IEnumerable<string> RangeLineItems { get; }
    
    public ProgramTestsFixture()
    {
        CalorieLineItems = ReadLineItems(TestData.CalorieLineItems);
        RucksackLineItems = ReadLineItems(TestData.RucksackLineItems);
        RpsLineItems = ReadLineItems(TestData.RpsLineItems);
        RangeLineItems = ReadLineItems(TestData.RangeLineItems);
    }

    private static IEnumerable<string> ReadLineItems(string items)
    {
        return Functions.ReadLines(new StringReader(items)).ToArrayAsync().Result;
    }
}