using UnityEngine;
using Zenject;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour
{
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite normalSprite;
    
    private SpriteRenderer _spriteRenderer;
    private BirdMover _mover;
    private SignalBus _signalBus;
    private int _score;
    private int _coins;
    
    public int Coins => _coins;
    public bool isArmored;
    
    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        _mover = GetComponent<BirdMover>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetPlayer(bool resetStats = true)
    {
        if (resetStats)
        {
            _score = 0;
            _signalBus.Fire(new ScoreChangedSignal(_score));

            _coins = 0;
            _signalBus.Fire(new CoinCountChangedSignal(_score));    
        }
        _mover.ResetBird();
        _spriteRenderer.sprite = normalSprite;
    }

    public void IncrementScore()
    {
        _score++;
        _signalBus.Fire(new ScoreChangedSignal(_score));
    }
    
    public void IncrementCoins()
    {
        _coins++;
        _signalBus.Fire(new CoinCountChangedSignal(_coins));
    }
    
    public void DecrementCoins(int amount)
    {
        _coins -= amount;
        _signalBus.Fire(new CoinCountChangedSignal(_coins));
    }
    
    public void Die()
    {
        _mover.DisableAnimator();
        _spriteRenderer.sprite = deadSprite;
        _signalBus.Fire(new GameStateChangedSignal(GameState.GameOver));
    }
    
    public void GetDamage()
    {
        isArmored = false;
        _mover.ShowDamage();
    }
    
}
