using AdventOfCode.Shared;
using AdventOfCode.Shared.Types.RPS;

namespace AdventOfCode.Project.Tests;

public class FunctionsTests : IClassFixture<ProgramTestsFixture>
{
    private readonly ProgramTestsFixture _fixture;

    public FunctionsTests(ProgramTestsFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public void CanReadLinesFromInput()
    {
        var lines = _fixture.CalorieLineItems.ToArray();
        
        lines.Length.Should().Be(9);
        lines.SequenceEqual(new[] { "100", "", "200", "300", "400", "", "50", "60", "" }).Should().BeTrue();
    }

    [Fact]
    public void CanCreateLineItems()
    {
        var lines = _fixture.CalorieLineItems.ToArray();
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
        var lines = _fixture.RucksackLineItems.ToArray();
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

    [Fact]
    public void CanGroupRucksacks()
    {
        var lines = _fixture.RucksackLineItems.ToArray();
        var rucksacks = lines.CreateRucksacks().ToArray();
        const int groupSize = 3;

        var groups = rucksacks.DivideIntoGroupsOf(groupSize).Single().ToArray();
        
        groups.Length.Should().Be(groupSize);
        
        groups.Select(r => r.Id)
            .SequenceEqual(new[]
                { "vJrwpWtwJgWrhcsFMMfFFhFp", "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "PmmdzqPrVvPwwTWBwg" })
            .Should().Be(true);
    }

    [Fact]
    public void CanGetCommonCharactersInGroup()
    {
        var lines = _fixture.RucksackLineItems.ToArray();
        var rucksacks = lines.CreateRucksacks().ToArray();
        const int groupSize = 3;

        var group = rucksacks.DivideIntoGroupsOf(groupSize).Single().ToArray();

        var ids = group.Select(r => r.Id).ToArray();

        ids.Length.Should().Be(groupSize);

        var intersect = ids.IntersectAll();

        intersect.Single().Should().Be('r');
    }
    
    [Fact]
    public void CanPlayRockPaperScissors()
    {
        var lines = _fixture.RpsLineItems;
        var games = RpsGameFactory.CreateGames(lines).ToArray();

        games.Length.Should().Be(3);
        
        var firstGame = games.First();
        firstGame.OpponentMove.Should().Be(RpsChoice.Rock);
        firstGame.MyMove.Should().Be(RpsChoice.Rock);
        firstGame.Result.Should().Be(RpsResult.Draw);
        firstGame.Score.Should().Be((int)RpsResult.Draw + (int)RpsChoice.Rock);
        
        var secondGame = games.ElementAt(1);
        secondGame.OpponentMove.Should().Be(RpsChoice.Paper);
        secondGame.MyMove.Should().Be(RpsChoice.Rock);
        secondGame.Result.Should().Be(RpsResult.Lose);
        secondGame.Score.Should().Be((int)RpsResult.Lose + (int)RpsChoice.Rock);
        
        var thirdGame = games.ElementAt(2);
        thirdGame.OpponentMove.Should().Be(RpsChoice.Scissors);
        thirdGame.MyMove.Should().Be(RpsChoice.Rock);
        thirdGame.Result.Should().Be(RpsResult.Win);
        thirdGame.Score.Should().Be((int)RpsResult.Win + (int)RpsChoice.Rock);
    }

    [Fact]
    public void ToRpsChoiceThrowsOnInvalidString()
    {
        Action a = () => RpsChoiceExtensions.ToRpsChoice("foo");

        a.Should().Throw<InvalidOperationException>().WithMessage("'foo' is an invalid value.");
    }
    
    [Fact]
    public void ToRpsChoiceFromRpsResultThrowsOnInvalidString()
    {
        Action a = () => RpsResultExtensions.ToRpsChoice("foo");

        a.Should().Throw<InvalidOperationException>().WithMessage("'foo' is an invalid value.");
    }

    [Fact]
    public void CreateRucksackThrowsOnInvalidCharacter()
    {
        var lines = new [] {"1ab1"};

        var func = () => lines.CreateRucksacks();

        func.Enumerating()
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("'1' is not a valid character.");

    }
}