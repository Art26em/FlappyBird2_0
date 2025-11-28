using UnityEngine;
using Zenject;

public class Bird
{
    private SignalBus _signalBus;
    private BirdSpriteController _birdSpriteController;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Wallet _wallet;
    private readonly ScoreManager _scoreManager;
    
    public bool IsArmored;
    
    [Inject]
    public void Construct(SignalBus signalBus, BirdSpriteController birdSpriteController)
    {
        _signalBus = signalBus;
        _birdSpriteController = birdSpriteController;
    }

    public Bird()
    {
        _spriteRenderer = new SpriteRenderer();
        _wallet = new Wallet();
        _scoreManager = new ScoreManager();
    }
    
    public void ResetPlayer(bool resetStats = true)
    {
        if (resetStats)
        {
            _scoreManager.ResetScore();
            _wallet.ResetCoins();   
        }
        _birdSpriteController.SetNormalSprite(_spriteRenderer);
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
        _birdSpriteController.SetDeadSprite(_spriteRenderer);
        _signalBus.Fire(new GameStateChangedSignal(GameState.GameOver));
    }
    
    public void GetDamage()
    {
        IsArmored = false;
    }
    
}
