using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem collectEffect;

    private void OnDisable()
    {
        collectEffect.Play();
    }
}
