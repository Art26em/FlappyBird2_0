using Zenject;

public class Wallet
{
    private int _coinsAmount;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    public int GetCoinsAmount()
    {
        return _coinsAmount;
    }
    
    public void IncrementCoins()
    {
        _coinsAmount++;
        _signalBus.Fire(new CoinCountChangedSignal(_coinsAmount));
    }
    
    public void DecrementCoins(int amount)
    {
        _coinsAmount -= amount;
        _signalBus.Fire(new CoinCountChangedSignal(_coinsAmount));
    }
    
    public void ResetCoins()
    {
        _coinsAmount = 0;
        _signalBus.Fire(new CoinCountChangedSignal(_coinsAmount));
    }
    
}