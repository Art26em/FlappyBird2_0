using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BirdMover))]

public class Bird : MonoBehaviour
{
    private BirdMover _mover;
    private int _score;

    public event UnityAction GameOver;
    public event UnityAction<int> UpdateScore;
    
    private void Awake()
    {
        _mover = GetComponent<BirdMover>();
    }

    public void ResetPlayer()
    {
        _score = 0;
        UpdateScore?.Invoke(_score);
        _mover.ResetBird();
    }

    public void IncrementScore()
    {
        _score++;
        UpdateScore?.Invoke(_score);
    }
    
    public void Die()
    {
        GameOver?.Invoke();
    }
    
}
