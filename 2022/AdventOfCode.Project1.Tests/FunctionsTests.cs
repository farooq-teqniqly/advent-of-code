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
        
        lines.Length.Should().Be(6);
        lines.SequenceEqual(new[] { "100", "", "200", "300", "400", "" }).Should().BeTrue();
    }

    [Fact]
    public void CanCreateGroups()
    {
        var groups = fixture.Groups.ToArray();
        
        groups.Length.Should().Be(2);
        groups.Select(g => g.Index).ToArray().SequenceEqual(new[] { 1, 2 }).Should().BeTrue();
        groups.First().Data.Select(d => d).ToArray().SequenceEqual(new[] { 100 }).Should().BeTrue();
        groups.Last().Data.Select(d => d).ToArray().SequenceEqual(new[] { 200, 300, 400 }).Should().BeTrue();
    }

    [Fact]
    public void CanGetGroupTotals()
    {
        var totals = fixture.GroupTotals.ToArray();
        
        totals.Length.Should().Be(2);
        totals.Select(t => t.Index).ToArray().SequenceEqual(new[] { 1, 2 }).Should().BeTrue();
        totals.First().Total.Should().Be(100);
        totals.Last().Total.Should().Be(900);
    }

    [Fact]
    public void CanGetGroupWithMaxTotal()
    {
        var max = Functions.GetMaxGroup(fixture.GroupTotals);
        max.Index.Should().Be(2);
        max.Total.Should().Be(900);
    }
}