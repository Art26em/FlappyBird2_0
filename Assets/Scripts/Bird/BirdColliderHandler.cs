using UnityEngine;
using Zenject;

[RequireComponent(typeof(Bird))]
public class BirdColliderHandler : MonoBehaviour
{
    private Bird _bird;

    [Inject]
    private void Construct(Bird bird)
    {
        _bird = bird;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ScoreZone _))
        {
            _bird.IncrementScore();    
        }
        else if (other.TryGetComponent(out Coin coin))
        {
            _bird.IncrementCoins();
            coin.gameObject.SetActive(false);
        }
        else
        {
            _bird.Die();    
        }
    }
}
