using UnityEngine;
using Zenject;

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
            _bird.OnScoreZonePassed();    
        }
        else if (other.TryGetComponent(out Coin coin))
        {
            _bird.OnCoinCollected();
            coin.OnCollect();
        }
        else
        {
            if (_bird.IsArmored)
            {
                _bird.GetDamage();
            }
            else
            {
                _bird.Die();    
            }
        }
    }
}
