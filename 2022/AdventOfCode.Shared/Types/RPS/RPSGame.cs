namespace AdventOfCode.Shared.Types.RPS;

public class RpsGame
{
    public RpsGame(string line)
    {
        Setup(line);
        Play();
    }
    
    public RpsChoice OpponentMove { get; set; }
    public RpsChoice MyMove { get; set; }
    public RpsResult Result { get; set; }
    
    public int Score { get; set; }

    private void Setup(string line)
    {
        var split = line.Split(' ');
        OpponentMove = split[0].ToRpsChoice();
        MyMove = split[1].ToRpsChoice();
    }
    
    private void Play()
    {
        if (DoIDraw())
        {
            Result = RpsResult.Draw;
        }
        else
        {
            Result = DoIWin() ? RpsResult.Win : RpsResult.Lose;
        }
        
        Score = (int)MyMove + (int)Result;
    }

    private bool DoIDraw()
    {
        return OpponentMove == MyMove;
    }

    private bool DoIWin()
    {
        return OpponentMove switch
        {
            RpsChoice.Rock => MyMove == RpsChoice.Paper,
            RpsChoice.Paper => MyMove == RpsChoice.Scissors,
            _ => MyMove == RpsChoice.Rock
        };
    }
}