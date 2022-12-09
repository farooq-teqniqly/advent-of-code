using AdventOfCode.Project;
using FluentAssertions;

namespace AdventOfCode.Project2.Tests;

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

        var top3Groups = totals.SortDescending().Top(3);
        var totalCalories = top3Groups.Select(g => g.Total).Sum();

        totalCalories.Should().Be(200116);
    }
}