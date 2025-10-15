using UnityEngine;

[RequireComponent(typeof(Bird))]

public class BirdColliderHandler : MonoBehaviour
{
    [SerializeField] private Bird bird;

    private void Awake()
    {
        bird = GetComponent<Bird>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ScoreZone scoreZone))
        {
            bird.IncrementScore();    
        }
        else
        {
            bird.Die();    
        }
    }
}
