namespace AdventOfCode.Shared.Types.ShelfGame;

public static class ShelfFactory
{
    public static IEnumerable<Shelf> CreateShelves(this string[,] matrix)
    {
        var rowCount = matrix.GetLength(0);
        var colCount = matrix.GetLength(1);
        var items = new string[colCount];

        for (var col = 0; col < colCount; col++)
        {
            for (var row = rowCount - 1; row >= 0; row--)
            {
                items[rowCount - row - 1] = matrix[row, col];
            }
            
            yield return new Shelf(items, col + 1);
        }
    }
}