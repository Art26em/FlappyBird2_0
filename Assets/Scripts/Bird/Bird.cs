using UnityEngine;
using Zenject;

public class Bird
{
    private SignalBus _signalBus;
    private ScoreManager _scoreManager;
    private Wallet _wallet;
    
    public bool IsArmored;
    
    [Inject]
    public void Construct(
        SignalBus signalBus, 
        ScoreManager scoreManager, 
        Wallet wallet)
    {
        _signalBus = signalBus;
        _scoreManager = scoreManager;
        _wallet = wallet;
    }
    
    public void ResetPlayer(bool resetStats = true)
    {
        if (!resetStats) return;
        _scoreManager.ResetScore();
        _wallet.ResetCoins();
    }

    public void OnScoreZonePassed()
    {
        _scoreManager.IncrementScore();
    }

    public void OnCoinCollected()
    {
        _wallet.IncrementCoins();
    }

    public int GetCoinsAmount()
    {
        return _wallet.GetCoinsAmount();
    }

    public void OnItemPurchase(int amount)
    {
        _wallet.DecrementCoins(amount);
    }
    
    public void Die()
    {
        _signalBus.Fire(new GameStateChangedSignal(GameState.GameOver));
    }
    
    public void GetDamage()
    {
        IsArmored = false;
        _signalBus.Fire(new BirdDamagedSignal());
    }
    
}
