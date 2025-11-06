using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectEffect;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        _spriteRenderer.enabled = true;
        _collider.enabled = true;
    }
    
    public void OnCollect()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        collectEffect.Play();
    }
    
}
