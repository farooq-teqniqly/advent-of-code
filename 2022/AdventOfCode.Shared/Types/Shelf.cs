namespace AdventOfCode.Shared.Types;

public class Shelf
{
    private readonly Stack<string> _stack = new();
    private readonly int _id;

    public int Id => _id;
    public IEnumerable<string> Items => _stack.AsEnumerable();

    public Shelf(IEnumerable<string> items, int id)
    {
        _id = id;
        
        foreach (var item in items)
        {
            if (item is "[0]" or null)
            {
                continue;
            }
            
            _stack.Push(item);
        }
    }
}

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