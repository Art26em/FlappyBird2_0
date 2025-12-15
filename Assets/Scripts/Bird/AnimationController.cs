using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class AnimationController : IInitializable, IDisposable
{
    private readonly AnimationSettings _animationSettings;
    private Sequence _blinkSequence;
    private Color _originalColor;
    private readonly string _birdFlyAnimationName = AnimationNames.BirdFly;

    private Animator _animator;
    private SpriteRenderer _renderer;
    private CircleCollider2D _collider;
    private float _elapsedTime;
    private BirdSpriteController _birdSpriteController;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(BirdSpriteController birdSpriteController, SignalBus signalBus)
    {
        _birdSpriteController = birdSpriteController;
        _signalBus = signalBus;
    }
    
    public AnimationController(AnimationSettings animationSettings)
    {
        _animationSettings = animationSettings;
    }
    
    public void Initialize()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
        _signalBus.Subscribe<BirdDamagedSignal>(OnBirdDamaged);
    }
    
    public void SetSpriteRenderer(SpriteRenderer renderer)
    {
        _renderer = renderer;
        _originalColor = renderer.color;
    }

    public void SetCollider(CircleCollider2D collider)
    {
        _collider = collider;
    }
    
    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    public float GetStartAnimationDuration()
    {
        return _animationSettings.startAnimationDuration;
    }
    
    public bool IsStartAnimationPerforming(float elapsedTime)
    {
        return elapsedTime < _animationSettings.startAnimationDuration;
    }

    public void ShowFlyAnimation()
    {
        _animator.Play(_birdFlyAnimationName);
    }

    private void OnBirdDamaged()
    {
        _elapsedTime = 0;
        _ = AnimateDamage();
    }

    private async UniTask AnimateDamage()
    {
        _collider.enabled = false;
        var alpha = true;
        while (_elapsedTime < _animationSettings.blinkAnimationDuration)
        {
            alpha = !alpha;
            _renderer.color = new Color(1, 1, 1, (alpha ? 1 : 0));
            _elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }
        _collider.enabled = true;
        _renderer.color = _originalColor;
    }
    
    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        switch (signal.NewState)
        {
            case GameState.Playing or GameState.Starting:
                _animator.enabled = true;
                _birdSpriteController.SetNormalSprite(ref _renderer);
                break;
            case GameState.GameOver:
                _animator.enabled = false;
                _birdSpriteController.SetDeadSprite(ref _renderer);
                break;
        }
    }
    
    public void Dispose()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
}