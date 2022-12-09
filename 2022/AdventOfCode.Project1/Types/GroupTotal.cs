namespace AdventOfCode.Project1.Types;

public record GroupTotal(int Index, int Total) : IComparable<GroupTotal>
{
    public int CompareTo(GroupTotal? other)
    {
        return other != null ? Total.CompareTo(other.Total) : Total;
    }
}