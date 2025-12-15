using System;
using UnityEngine;
using Unity.VisualScripting;
using Zenject;
using Sequence = DG.Tweening.Sequence;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]

public class BirdMover : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider2D;
    
    private float _elapsedTime;
    private Animator _animator;
    private Sequence _blinkSequence;
    
    private StateController _stateController;
    private MovementController _movementController;
	private AnimationController _animationController;    

    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(
        StateController stateController,  
        MovementController movementController,
        AnimationController animationController,
        SignalBus signalBus)
    {
        _stateController = stateController;    
        _movementController = movementController;
        _animationController = animationController;
        _signalBus = signalBus;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _movementController.SetPlayer(transform);
        _movementController.SetRigidbody(_rigidbody);
        
        _animationController.SetAnimator(_animator);
        _animationController.SetSpriteRenderer(_spriteRenderer);
        _animationController.SetCollider(_circleCollider2D);

        ResetPlayer();
    }
    
    private void OnEnable()
    {
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }
    
    private void OnGameStateChanged(GameStateChangedSignal signal)
    {
        switch (signal.NewState)
        {
            case GameState.Starting or GameState.Playing:
                ResetPlayer();
                break;
        }
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        var isStartAnimationPerforming = _animationController.IsStartAnimationPerforming(_elapsedTime);
        
        switch (isStartAnimationPerforming)
        {
            case true:
                _movementController.ResetVelocity();
                _movementController.ResetRotation();
                break;
            case false when _stateController.CurrentGameState is GameState.Playing or GameState.Starting:
                var moved = _movementController.TryMove();
                if (moved) _animationController.ShowFlyAnimation();
                _movementController.Rotate();
                break;
        }
    }
    
    private void ResetPlayer()
    {
        _movementController.ResetPlayer(_animationController.GetStartAnimationDuration());
        _elapsedTime = 0;
    }
    
    private void OnDisable()
    {
        _signalBus.Unsubscribe<GameStateChangedSignal>(OnGameStateChanged);    
    }
    
}
