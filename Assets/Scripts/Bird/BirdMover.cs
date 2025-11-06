using System.Collections;
using UnityEngine;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private float startAnimationDuration = 1f;
    [SerializeField] private float blinkAnimationDuration = 3f;
    [SerializeField] private float tapForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float minRotationZ;
    
    private Rigidbody2D _rigidbody;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider2D;
    
    private float _elapsedTime;
    private Animator _animator;
    private Sequence _blinkSequence;
    private Color _originalColor;
    
    private readonly string _birdFlyAnimationName = AnimationNames.BirdFly;

    private GameManager _gameManager;
    
    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;    
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _minRotation = Quaternion.Euler(0, 0, minRotationZ);
        _maxRotation = Quaternion.Euler(0, 0, maxRotationZ);
        ResetBird();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime < startAnimationDuration)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            && _gameManager.CurrentGameState == GameState.Playing)
        {
            _animator.Play(_birdFlyAnimationName);
            transform.rotation = _maxRotation;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, Time.deltaTime * rotationSpeed);    
    }

    public void ResetBird()
    {
        transform.position = startPosition;
	 	transform.DOMove(offsetPosition, startAnimationDuration);
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _elapsedTime = 0;
        _animator.enabled = true;
    }
    
    public void DisableAnimator()
    {
        _animator.enabled = false;
    }

    public void ShowDamage()
    {
        _elapsedTime = 0;
        StartCoroutine(AnimateDamage());
    }

    private IEnumerator AnimateDamage()
    {
        _circleCollider2D.enabled = false;
        bool alpha = true;
        while (_elapsedTime < blinkAnimationDuration)
        {
            alpha = !alpha;
            _spriteRenderer.color = new Color(1, 1, 1, (alpha ? 1 : 0));
            yield return null;
        }
        _circleCollider2D.enabled = true;
        _spriteRenderer.color = _originalColor;
    }
    
}
