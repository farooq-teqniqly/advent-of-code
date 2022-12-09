using AdventOfCode.Project;
using AdventOfCode.Tests.Shared;

namespace AdventOfCode.Project1.Tests;

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
        var lines = fixture.Lines.ToArray();
        
        lines.Length.Should().Be(9);
        lines.SequenceEqual(new[] { "100", "", "200", "300", "400", "", "50", "60", "" }).Should().BeTrue();
    }

    [Fact]
    public void CanCreateGroups()
    {
        var groups = fixture.Groups.ToArray();
        
        groups.Length.Should().Be(3);
        groups.Select(g => g.Index).ToArray().SequenceEqual(new[] { 1, 2, 3 }).Should().BeTrue();
        groups.First().Data.Select(d => d).ToArray().SequenceEqual(new[] { 100 }).Should().BeTrue();
        groups.ElementAt(1).Data.Select(d => d).ToArray().SequenceEqual(new[] { 200, 300, 400 }).Should().BeTrue();
        groups.Last().Data.Select(d => d).ToArray().SequenceEqual(new[] { 50, 60 }).Should().BeTrue();
    }

    [Fact]
    public void CanGetGroupTotals()
    {
        var totals = fixture.GroupTotals.ToArray();
        
        totals.Length.Should().Be(3);
        totals.Select(t => t.Index).ToArray().SequenceEqual(new[] { 1, 2, 3 }).Should().BeTrue();
        totals.First().Total.Should().Be(100);
        totals.ElementAt(1).Total.Should().Be(900);
        totals.Last().Total.Should().Be(110);
    }

    [Fact]
    public void CanGetGroupWithMaxTotal()
    {
        var max = Functions.GetMaxGroup(fixture.GroupTotals);
        max.Index.Should().Be(2);
        max.Total.Should().Be(900);
    }
}