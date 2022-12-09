namespace AdventOfCode.Project1.Tests;

public class IntegrationTests
{
    [Fact]
    public async Task CanSolvePuzzle()
    {
        var reader = new StreamReader("puzzle.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var groups = Functions.CreateGroups(lines);
        var totals = Functions.GetGroupTotals(groups);
        var maxGroup = Functions.GetMaxGroup(totals);

        maxGroup.Index.Should().Be(189);
        maxGroup.Total.Should().Be(67016);
    }
}