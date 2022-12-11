namespace AdventOfCode.Shared.Types;

public record Rucksack(string[] Compartments, int Priority)
{
    public string Id => $"{Compartments[0]}{Compartments[1]}";
}