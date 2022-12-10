namespace AdventOfCode.Shared.Types.RPS;

public class RpsGame
{
    public RpsGame(string line)
    {
        Setup(line);
        Play();
    }
    
    public RpsChoice OpponentMove { get; private set; }
    public RpsChoice MyMove { get; private set; }
    public RpsResult Result { get; private set; }
    public int Score { get; private set; }

    private void Setup(string line)
    {
        var split = line.Split(' ');
        OpponentMove = RpsChoiceExtensions.ToRpsChoice(split[0]);
        Result = RpsResultExtensions.ToRpsChoice(split[1]);
    }
    
    private void Play()
    {
        MakeMyMove();
        Score = (int)MyMove + (int)Result;
    }

    private void MakeMyMove()
    {
        switch (OpponentMove)
        {
            case RpsChoice.Rock when Result == RpsResult.Draw:
                MyMove = RpsChoice.Rock;
                return;
            case RpsChoice.Rock when Result == RpsResult.Win:
                MyMove = RpsChoice.Paper;
                return;
            case RpsChoice.Rock:
                MyMove = RpsChoice.Scissors;
                return;
            case RpsChoice.Paper when Result == RpsResult.Draw:
                MyMove = RpsChoice.Paper;
                return;
            case RpsChoice.Paper when Result == RpsResult.Win:
                MyMove = RpsChoice.Scissors;
                return;
            case RpsChoice.Paper:
                MyMove = RpsChoice.Rock;
                return;
            case RpsChoice.Scissors:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (Result)
        {
            case RpsResult.Draw:
                MyMove = RpsChoice.Scissors;
                return;
            case RpsResult.Win:
                MyMove = RpsChoice.Rock;
                return;
            case RpsResult.Lose:
            default:
                MyMove = RpsChoice.Paper;
                break;
        }
    }
}