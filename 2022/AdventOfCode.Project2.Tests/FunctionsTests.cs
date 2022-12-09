using AdventOfCode.Project;
using AdventOfCode.Tests.Shared;
using FluentAssertions;

namespace AdventOfCode.Project2.Tests;

public class FunctionsTests : IClassFixture<ProgramTestsFixture>
{
    private readonly ProgramTestsFixture fixture;

    public FunctionsTests(ProgramTestsFixture fixture)
    {
        this.fixture = fixture;
    }
    [Fact]
    public void CanGetTheTopNGroups()
    {
        var top3Groups = fixture.GroupTotals.SortDescending().Top(3).ToArray();

        top3Groups.Length.Should().Be(3);
        top3Groups.Select(t => t.Index).ToArray().SequenceEqual(new[] { 2, 3, 1 }).Should().BeTrue();
        top3Groups.First().Total.Should().Be(900);
        top3Groups.ElementAt(1).Total.Should().Be(110);
        top3Groups.Last().Total.Should().Be(100);
    }
}