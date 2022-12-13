﻿using AdventOfCode.Shared.Types.RPS;

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
        var reader = new StreamReader("day3.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var sumOfPriorities = lines.CreateRucksacks().Sum(r => r.Priority);

        sumOfPriorities.Should().Be(8053);
    }
    
    [Fact]
    public async Task Day3Part2()
    {
        var reader = new StreamReader("day3.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var rucksacks = lines.CreateRucksacks().ToArray();

        rucksacks.Length.Should().Be(300);

        const int groupSize = 3;
        var groups = rucksacks.DivideIntoGroupsOf(groupSize).ToArray() ?? throw new ArgumentNullException("rucksacks.DivideIntoGroupsOf(groupSize).ToArray()");
        
        var totalPriority = 0;
        
        foreach (var group in groups)
        {
            totalPriority += group.Select(r => r.Id).IntersectAll().Single().ToPriority();
        }

        totalPriority.Should().Be(2425);
    }

    [Fact]
    public async Task Day4Part1()
    {
        var reader = new StreamReader("day4.txt");
        string[] lines;
        
        using (reader)
        {
            lines = await Functions.ReadLines(reader).ToArrayAsync();
        }

        var rangeList = lines.CreateRanges().ToArray();

        rangeList.Length.Should().Be(1000);

        var overlaps = rangeList
            .Select(rangePair =>
            {
                var arr = rangePair.ToArray();
                return arr
                    .First()
                    .FullOverlapExists(arr.ElementAt(1));
            })
            .Count(doesOverlap => doesOverlap);

        overlaps.Should().Be(584);
    }
}