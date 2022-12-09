using AdventOfCode.Project;

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
    public void CanCreateLineItems()
    {
        var lines = fixture.Lines.ToArray();
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
}