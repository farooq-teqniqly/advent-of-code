using AdventOfCode.Shared;
using AdventOfCode.Shared.Types;
using AdventOfCode.Shared.Types.RPS;

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

    [Fact]
    public void CanPlayRockPaperScissors()
    {
        var lines = fixture.RPSLineItems;
        var games = RpsGameFactory.CreateGames(lines).ToArray();

        games.Length.Should().Be(4);

        var firstGame = games.First();
        firstGame.OpponentMove.Should().Be(RpsChoice.Rock);
        firstGame.MyMove.Should().Be(RpsChoice.Paper);
        firstGame.Result.Should().Be(RpsResult.Win);
        firstGame.Score.Should().Be(8);
        
        var secondGame = games.ElementAt(1);
        secondGame.OpponentMove.Should().Be(RpsChoice.Paper);
        secondGame.MyMove.Should().Be(RpsChoice.Rock);
        secondGame.Result.Should().Be(RpsResult.Lose);
        secondGame.Score.Should().Be(1);
        
        var thirdGame = games.ElementAt(2);
        thirdGame.OpponentMove.Should().Be(RpsChoice.Scissors);
        thirdGame.MyMove.Should().Be(RpsChoice.Scissors);
        thirdGame.Result.Should().Be(RpsResult.Draw);
        thirdGame.Score.Should().Be(6);
        
        var lastGame = games.Last();
        lastGame.OpponentMove.Should().Be(RpsChoice.Scissors);
        lastGame.MyMove.Should().Be(RpsChoice.Rock);
        lastGame.Result.Should().Be(RpsResult.Win);
        lastGame.Score.Should().Be(7);
    }

    [Fact]
    public void ToRpsChoiceThrowsOnInvalidString()
    {
        Action a = () => "foo".ToRpsChoice();

        a.Should().Throw<InvalidOperationException>().WithMessage("'foo' is an invalid value.");
    }

    [Fact]
    public void CreateRucksackThrowsOnInvalidCharacter()
    {
        var lines = new [] {"1ab1"};

        Func<IEnumerable<Rucksack>> func = () => lines.CreateRucksacks();

        func.Enumerating()
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("'1' is not a valid character.");

    }
}