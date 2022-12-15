namespace AdventOfCode.Shared.Types.ShelfGame;

public class Shelf
{
    private readonly Stack<string> stack = new();

    public int Id { get; }

    public IEnumerable<string> Items => stack.AsEnumerable();

    public bool IsEmpty => !stack.Any();

    public Shelf(IEnumerable<string> items, int id)
    {
        Id = id;
        
        foreach (var item in items)
        {
            if (item is "[0]" or null)
            {
                continue;
            }
            
            stack.Push(item);
        }
    }

    public string? RemoveFromTop()
    {
        return IsEmpty ? null : stack.Pop();
    }

    public void AddToTop(string item)
    {
        stack.Push(item);
    }
}