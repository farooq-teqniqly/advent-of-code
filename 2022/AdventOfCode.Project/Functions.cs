using AdventOfCode.Shared.Types;

namespace AdventOfCode.Project;

public static class Functions
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
            var compartment1 = line[..(midpoint)];
            var compartment2 = line[(midpoint)..];
            var common = compartment1.Intersect(compartment2).Single();
            
            yield return new Rucksack(new[] { compartment1, compartment2 }, common.ToPriority());
        }
    }

    private static int ToPriority(this char c)
    {
        var intValueOfChar = (int)c;
        
        if (c >= 97 && c <= 122)
        {
            return intValueOfChar - 96;
        }
        else if (c >= 65 && c <= 90)
        {
            return  intValueOfChar - 38;
        }

        throw new InvalidOperationException($"'{c}' is not a valid character.");
    }
}