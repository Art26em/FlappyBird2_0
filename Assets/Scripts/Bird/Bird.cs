using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(SpriteRenderer))]
public class Bird : MonoBehaviour
{
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private Sprite normalSprite;
    
    private BirdMover _mover;
    private int _score;
    private int _coins;
    private SpriteRenderer _spriteRenderer;
    
    public event UnityAction GameOver;
    public event UnityAction<int> UpdateScore;
    public event UnityAction<int> UpdateCoins;
    
    private void Awake()
    {
        _mover = GetComponent<BirdMover>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ResetPlayer()
    {
        _score = 0;
        UpdateScore?.Invoke(_score);
        _mover.ResetBird();
        _spriteRenderer.sprite = normalSprite;
    }

    public void IncrementScore()
    {
        _score++;
        UpdateScore?.Invoke(_score);
    }
    
    public void IncrementCoins()
    {
        _coins++;
        UpdateCoins?.Invoke(_coins);
    }
    
    public void DecrementCoins(int amount)
    {
        _coins -= amount;
        UpdateCoins?.Invoke(_coins);
    }
    
    public void Die()
    {
        _mover.DisableAnimator();
        _spriteRenderer.sprite = deadSprite;
        GameOver?.Invoke();
    }
    
}
