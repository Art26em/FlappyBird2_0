
public struct CoinCountChangedSignal
{
    public int NewCoinCount { get; }

    public CoinCountChangedSignal(int newCoinCount)
    {
        NewCoinCount = newCoinCount;
    }
}
