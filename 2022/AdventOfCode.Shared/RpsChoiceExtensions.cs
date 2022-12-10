using AdventOfCode.Shared.Types.RPS;

namespace AdventOfCode.Shared;

public static class RpsChoiceExtensions
{
    public static RpsChoice ToRpsChoice(this string s) =>
        s switch
        {
            "A" => RpsChoice.Rock,
            "B" => RpsChoice.Paper,
            "C" => RpsChoice.Scissors,

            _ => throw new InvalidOperationException($"'{s}' is an invalid value.")
        };
}

public static class RpsResultExtensions
{
    public static RpsResult ToRpsChoice(this string s) =>
        s switch
        {
            "X" => RpsResult.Lose,
            "Y" => RpsResult.Draw,
            "Z" => RpsResult.Win,

            _ => throw new InvalidOperationException($"'{s}' is an invalid value.")
        };
}