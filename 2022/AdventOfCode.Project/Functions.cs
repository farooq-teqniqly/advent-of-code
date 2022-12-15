using System.Text.RegularExpressions;
using AdventOfCode.Shared.Types;
using AdventOfCode.Shared.Types.ShelfGame;

namespace AdventOfCode.Project;

public static partial class Functions
{
    public static async IAsyncEnumerable<string> ReadLines(TextReader reader)
    {
        while (await reader.ReadLineAsync() is { } line)
        {
            yield return line;
        }

        yield return string.Empty;
    }

    public static IEnumerable<LineItem> CreateLineItems(this IEnumerable<string> lines)
    {
        var index = 1;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                index++;
                continue;
            }

            yield return new LineItem(index, int.Parse(line));
        }
    }

    public static IEnumerable<Rucksack> CreateRucksacks(this IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            var midpoint = line.Length / 2;
            var compartment1 = line[..midpoint];
            var compartment2 = line[midpoint..];
            var common = compartment1.Intersect(compartment2).Single();
            
            yield return new Rucksack(new[] { compartment1, compartment2 }, common.ToPriority());
        }
    }

    public static IEnumerable<IEnumerable<Rucksack>> DivideIntoGroupsOf(this IEnumerable<Rucksack> rucksacks, int groupSize)
    {
        return rucksacks
            .Select((rucksack, index) => new { rucksack, index })
            .GroupBy(g => g.index / groupSize)
            .Select(g => g.Select(r => r.rucksack));
    }

    public static IEnumerable<char> IntersectAll(this IEnumerable<string> strings)
    {
        var arr = strings.ToArray();
        var intersect = arr.First().Intersect(arr.ElementAt(1));

        for (var i = 2; i < arr.Length; i++)
        {
            intersect = intersect.Intersect(arr.ElementAt(i));
        }

        return intersect;
    }
    
    public static int ToPriority(this char c)
    {
        var intValueOfChar = (int)c;
        
        if (c >= 97 && c <= 122)
        {
            return intValueOfChar - 96;
        }
        
        if (c >= 65 && c <= 90)
        {
            return  intValueOfChar - 38;
        }

        throw new InvalidOperationException($"'{c}' is not a valid character.");
    }

    public static bool FullOverlapExists(this IEnumerable<int> range1, IEnumerable<int> range2)
    {
        IEnumerable<int> shorterRange;
        IEnumerable<int> longerRange;
        
        var range1Arr = range1.ToArray();
        var range2Arr = range2.ToArray();

        if (range1Arr.Length < range2Arr.Length)
        {
            shorterRange = range1Arr;
            longerRange = range2Arr;
        }
        else
        {
            longerRange = range1Arr;
            shorterRange = range2Arr;
        }

        var intersect = longerRange.Intersect(shorterRange);
        return intersect.SequenceEqual(shorterRange);
    }

    public static IEnumerable<IEnumerable<IEnumerable<int>>> CreateRanges(this IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var pairs = line.Split(',');
            var ranges = new List<IEnumerable<int>>();
            var count = 0;
            
            foreach (var pair in pairs)
            {
                if (count == 2)
                {
                    yield return ranges;
                    count = 0;
                    ranges = new List<IEnumerable<int>>();
                }
                else
                {
                    var range = pair.Split('-').Select(int.Parse).ToArray();
                    ranges.Add(Enumerable.Range(range[0], range[1] - range[0] + 1));
                    count++;
                }
            }

            yield return ranges;
        }
    }

    public static string[,] CreateShelfMatrix(
        this IEnumerable<string> lines,
        int shelfCount,
        int shelfHeight)
    {
        var matrix = new string[shelfHeight, shelfCount];
        var lineNo = 0;
        
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            var colValues = line.Split(" ");
            
            for (var colNo = 0; colNo < shelfCount; colNo++)
            {
                matrix[lineNo, colNo] = colValues[colNo];
            }
    
            lineNo++;
        }

        return matrix;
    }

    [GeneratedRegex(@"move\s+(?<moves>\d{1,2})\s+from\s+(?<from>\d{1})\s+to\s+(?<to>\d{1})", 
        RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex MyRegex();
    public static IEnumerable<ShelfItemMove> CreateShelfItemMoves(this IEnumerable<string> lines)
    {
        var regex = MyRegex();
        
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            var matchResult = regex.Match(line);

            yield return new ShelfItemMove(
                int.Parse(matchResult.Groups["moves"].Value),
                int.Parse(matchResult.Groups["from"].Value),
                int.Parse(matchResult.Groups["to"].Value));

        }
    }

    public static void ApplyMoves(this IEnumerable<Shelf> shelves, IEnumerable<ShelfItemMove> moves)
    {
        var shelfArr = shelves.ToArray();
        
        foreach (var move in moves)
        {
            var fromShelf = shelfArr.ElementAt(move.FromId - 1);
            var toShelf = shelfArr.ElementAt(move.ToId - 1);

            for (var i = 0; i < move.NumberOfItems; i++)
            {
                var popped = fromShelf.RemoveFromTop();
                toShelf.AddToTop(popped ?? throw new InvalidOperationException());
            }
        }
    }
}