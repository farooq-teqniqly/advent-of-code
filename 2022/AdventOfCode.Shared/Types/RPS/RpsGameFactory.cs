namespace AdventOfCode.Shared.Types.RPS;

public static class RpsGameFactory
{
    public static IEnumerable<RpsGame> CreateGames(IEnumerable<string> lines)
    {
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            
            yield return new RpsGame(line);
        }
    }
}