using AdventOfCode.Shared.Types;

namespace AdventOfCode.Project.Tests;

public class FunctionsTests : IClassFixture<ProgramTestsFixture>
{
    private readonly ProgramTestsFixture fixture;

    public FunctionsTests(ProgramTestsFixture fixture)
    {
        this.fixture = fixture;
    }
    
    [Fact]
    public void CanReadLinesFromInput()
    {
        var lines = fixture.CalorieLineItems.ToArray();
        
        lines.Length.Should().Be(9);
        lines.SequenceEqual(new[] { "100", "", "200", "300", "400", "", "50", "60", "" }).Should().BeTrue();
    }

    [Fact]
    public void CanCreateLineItems()
    {
        var lines = fixture.CalorieLineItems.ToArray();
        var lineItems = lines.CreateLineItems().ToArray();

        var index1LineItems = lineItems.Where(li => li.Index == 1).ToArray();
        index1LineItems.Length.Should().Be(1);
        index1LineItems.Single().Value.Should().Be(100);
        
        var index2LineItems = lineItems.Where(li => li.Index == 2).ToArray();
        index2LineItems.Length.Should().Be(3);
        index2LineItems.Select(li => li.Value).ToArray().SequenceEqual(new[] { 200, 300, 400 }).Should().BeTrue();
        
        var index3LineItems = lineItems.Where(li => li.Index == 3).ToArray();
        index3LineItems.Length.Should().Be(2);
        index3LineItems.Select(li => li.Value).ToArray().SequenceEqual(new[] { 50, 60 }).Should().BeTrue();
    }

    [Fact]
    public void CanCreateRucksacks()
    {
        var lines = fixture.RucksackLineItems.ToArray();
        var rucksacks = lines.CreateRucksacks().ToArray();

        rucksacks.Length.Should().Be(3);

        var firstRucksack = rucksacks.First();
        firstRucksack.Compartments[0].Should().Be("vJrwpWtwJgWr");
        firstRucksack.Compartments[1].Should().Be("hcsFMMfFFhFp");
        firstRucksack.Priority.Should().Be(16);
        
        var secondRucksack = rucksacks.ElementAt(1);
        secondRucksack.Compartments[0].Should().Be("jqHRNqRjqzjGDLGL");
        secondRucksack.Compartments[1].Should().Be("rsFMfFZSrLrFZsSL");
        secondRucksack.Priority.Should().Be(38);
        
        var thirdRucksack = rucksacks.Last();
        thirdRucksack.Compartments[0].Should().Be("PmmdzqPrV");
        thirdRucksack.Compartments[1].Should().Be("vPwwTWBwg");
        thirdRucksack.Priority.Should().Be(42);
    }
}