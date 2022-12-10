using AdventOfCode.Shared.Types.RPS;

namespace AdventOfCode.Project.Tests;

public class AdventOfCodeTests
{
    [Fact]
    public async Task Day1Part1()
    {
        var reader = new StreamReader("day1.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var maxGroup = lines.CreateLineItems()
            .GroupBy(li => li.Index)
            .Select((grp, _) => new { Index = grp.Key, Total = grp.Sum(li => li.Value) })
            .OrderByDescending(a => a.Total)
            .First();

        maxGroup.Index.Should().Be(189);
        maxGroup.Total.Should().Be(67016);
    }
    
    [Fact]
    public async Task Day1Part2()
    {
        var reader = new StreamReader("day1.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var top3Groups = lines.CreateLineItems()
            .GroupBy(li => li.Index)
            .Select((grp, _) => new { Index = grp.Key, Total = grp.Sum(li => li.Value) })
            .OrderByDescending(grp => grp.Total)
            .Take(3);

        var totalCalories = top3Groups.Sum(grp => grp.Total);

        totalCalories.Should().Be(200116);
    }

    [Fact]
    public async Task Day2()
    {
        var reader = new StreamReader("day2.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var totalScore = RpsGameFactory
            .CreateGames(lines)
            .Sum(g => g.Score);

        totalScore.Should().Be(11696);
    }

    [Fact]
    public async Task Day3()
    {
        var reader = new StreamReader("day3-4.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var sumOfPriorities = lines.CreateRucksacks().Sum(r => r.Priority);

        sumOfPriorities.Should().Be(8053);
    }
}