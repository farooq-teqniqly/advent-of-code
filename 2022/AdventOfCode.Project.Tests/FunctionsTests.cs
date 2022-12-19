using AdventOfCode.Shared;
using AdventOfCode.Shared.Types.RPS;
using AdventOfCode.Shared.Types.ShelfGame;

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
    public void CanGroupRucksacks()
    {
        var lines = fixture.RucksackLineItems.ToArray();
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
        var lines = fixture.RucksackLineItems.ToArray();
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
        var lines = fixture.RpsLineItems;
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

    [Fact]
    public void CanCreateRanges()
    {
        var lines = fixture.RangeLineItems;
        var rangeList = lines.CreateRanges().ToArray();

        rangeList.Length.Should().Be(2);

        var firstRangeList = rangeList.First().ToArray();
        firstRangeList.First().First().Should().Be(22);
        firstRangeList.First().Last().Should().Be(65);
        firstRangeList.Last().First().Should().Be(22);
        firstRangeList.Last().Last().Should().Be(66);
        
        var secondRangeList = rangeList.Last().ToArray();
        secondRangeList.First().First().Should().Be(91);
        secondRangeList.First().Last().Should().Be(94);
        secondRangeList.Last().First().Should().Be(63);
        secondRangeList.Last().Last().Should().Be(91);
    }

    [Theory]
    [InlineData(4, 6, 6, 6, true)]
    [InlineData(2, 8, 3, 7, true)]
    [InlineData(2, 4, 6, 8, false)]
    [InlineData(1, 1, 2, 2, false)]
    public void CanDetermineIfRangeIsFullyContainedByAnother(
        int range1Lo,
        int range1Hi,
        int range2Lo,
        int range2Hi,
        bool result)
    {
        Enumerable.Range(range1Lo, range1Hi - range1Lo + 1)
            .FullOverlapExists(Enumerable.Range(range2Lo, range2Hi - range2Lo + 1))
            .Should()
            .Be(result);
    }

    [Theory]
    [InlineData(2, 4, 6, 8, false)]
    [InlineData(2, 3, 4, 5, false)]
    [InlineData(5, 7, 7, 9, true)]
    [InlineData(2, 8, 3, 7, true)]
    [InlineData(6, 6, 4, 6, true)]
    [InlineData(2, 6, 4, 8, true)]
    public void CanDetermineIfThereIsAnyOverlap(
        int range1Lo,
        int range1Hi,
        int range2Lo,
        int range2Hi,
        bool result)
    {
        Enumerable.Range(range1Lo, range1Hi - range1Lo + 1)
            .Intersect(Enumerable.Range(range2Lo, range2Hi - range2Lo + 1))
            .Any()
            .Should().Be(result);
    }

    [Theory]
    [InlineData(1, new[] {"[W]", "[B]", "[G]"})]
    [InlineData(2, new[] {"[V]", "[T]", "[S]"})]
    [InlineData(3, new string[] {})]
    [InlineData(4, new[] {"[P]", "[C]", "[V]"})]
    [InlineData(5, new[] {"[B]", "[H]"})]
    [InlineData(6, new[] {"[N]"})]
    [InlineData(7, new[] {"[G]", "[T]"})]
    [InlineData(8, new[] {"[A]", "[X]"})]
    [InlineData(9, new[] {"[R]", "[L]"})]
    public void CanCreateShelves(int shelfId, string[] items)
    {
        var lines = fixture.ShelfLineItems.ToArray();
        var matrix = lines.CreateShelfMatrix(9, 3);
        var shelves = matrix.CreateShelves().ToArray();
        
        shelves.Single(s => s.Id == shelfId).Items.SequenceEqual(items).Should().BeTrue();
    }

    [Fact]
    public void CanCreateShelfItemMoves()
    {
        var lines = fixture.ShelfItemMoveLineItems.ToArray();
        var moves = lines.CreateShelfItemMoves().ToArray();

        moves.Length.Should().Be(3);

        var firstMove = moves.First();
        firstMove.NumberOfItems.Should().Be(2);
        firstMove.FromId.Should().Be(8);
        firstMove.ToId.Should().Be(4);
        
        var secondMove = moves.ElementAt(1);
        secondMove.NumberOfItems.Should().Be(2);
        secondMove.FromId.Should().Be(7);
        secondMove.ToId.Should().Be(3);
        
        var lastMove = moves.ElementAt(2);
        lastMove.NumberOfItems.Should().Be(2);
        lastMove.FromId.Should().Be(9);
        lastMove.ToId.Should().Be(2);
    }

    [Theory]
    [InlineData(1, new[] {"[W]", "[B]", "[G]"})]
    [InlineData(2, new[] {"[L]", "[R]", "[V]", "[T]", "[S]"})]
    [InlineData(3, new[] {"[T]", "[G]"})]
    [InlineData(4, new[] {"[X]", "[A]", "[P]", "[C]", "[V]"})]
    [InlineData(5, new[] {"[B]", "[H]"})]
    [InlineData(6, new[] {"[N]"})]
    [InlineData(7, new string[] {})]
    [InlineData(8, new string[] {})]
    [InlineData(9, new string[] {})]
    public void CanMoveShelfItems(int shelfId, string[] items)
    {
        var matrix = fixture.ShelfLineItems.CreateShelfMatrix(9, 3);
        var shelves = matrix.CreateShelves().ToArray();
        var moves = fixture.ShelfItemMoveLineItems.CreateShelfItemMoves().ToArray();

        shelves.ApplyMoves(moves);
        
        shelves.Single(s => s.Id == shelfId).Items.SequenceEqual(items).Should().BeTrue();
    }
    
    [Theory]
    [InlineData(1, new[] {"[W]", "[B]", "[G]"})]
    [InlineData(2, new[] {"[R]", "[L]", "[V]", "[T]", "[S]"})]
    [InlineData(3, new[] {"[G]", "[T]"})]
    [InlineData(4, new[] {"[A]", "[X]", "[P]", "[C]", "[V]"})]
    [InlineData(5, new[] {"[B]", "[H]"})]
    [InlineData(6, new[] {"[N]"})]
    [InlineData(7, new string[] {})]
    [InlineData(8, new string[] {})]
    [InlineData(9, new string[] {})]
    public void CanMoveShelfItemsV2(int shelfId, string[] items)
    {
        var matrix = fixture.ShelfLineItems.CreateShelfMatrix(9, 3);
        var shelves = matrix.CreateShelves().ToArray();
        var moves = fixture.ShelfItemMoveLineItems.CreateShelfItemMoves().ToArray();

        shelves.ApplyMovesV2(moves);
        
        shelves.Single(s => s.Id == shelfId).Items.SequenceEqual(items).Should().BeTrue();
    }
}