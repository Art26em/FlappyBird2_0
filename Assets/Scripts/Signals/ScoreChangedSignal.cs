
public struct ScoreChangedSignal
{
    public int NewScore { get; }

    public ScoreChangedSignal(int newScore)
    {
        NewScore = newScore;
    }
}