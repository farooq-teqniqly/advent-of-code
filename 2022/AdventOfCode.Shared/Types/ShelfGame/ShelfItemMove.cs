namespace AdventOfCode.Shared.Types.ShelfGame;

public class ShelfItemMove
{
    public int NumberOfItems { get; }
    public int FromId { get; }
    public int ToId { get; }

    public ShelfItemMove(int numberOfItems, int fromId, int toId)
    {
        NumberOfItems = numberOfItems;
        FromId = fromId;
        ToId = toId;
    }
}