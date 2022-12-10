using AdventOfCode.Shared.Types;
using AdventOfCode.Shared.Types.RPS;

namespace AdventOfCode.Shared;

public static class Extensions
{
    public static RpsChoice ToRpsChoice(this string s) =>
        s switch
        {
            ("A") or ("X") => RpsChoice.Rock,
            ("B") or ("Y") => RpsChoice.Paper,
            ("C") or ("Z") => RpsChoice.Scissors,

            _ => throw new InvalidOperationException($"'{s}' is an invalid value.")
        };
}