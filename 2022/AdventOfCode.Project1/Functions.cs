using AdventOfCode.Project1.Types;

namespace AdventOfCode.Project1;

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

    public static IEnumerable<Group> CreateGroups(IEnumerable<string> lines)
    {
        var groups = new List<Group>();
        List<int>? groupData = null;
        var index = 1;
        
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                var group = new Group(index, groupData ?? throw new InvalidOperationException());
                index++;
                groups.Add(group);
                groupData = null;
            }
            else
            {
                groupData ??= new List<int>();
                groupData.Add(int.Parse(line));
            }
        }

        return groups;
    }

    public static IEnumerable<GroupTotal> GetGroupTotals(IEnumerable<Group> groups)
    {
        return groups.Select(g => new GroupTotal(g.Index, g.Data.Sum()));
    }

    public static GroupTotal GetMaxGroup(IEnumerable<GroupTotal> groupTotals)
    {
        return groupTotals.Max() ?? throw new InvalidOperationException();
    }
}