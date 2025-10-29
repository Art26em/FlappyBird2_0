using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float tapForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float minRotationZ;
    
    private Rigidbody2D _rigidbody;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;
    private float _elapsedTime;
    private Animator _animator;
    
    private readonly string _birdFlyAnimationName = AnimationNames.BirdFly;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
        if (_elapsedTime < animationDuration)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0)
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
	 	transform.DOMove(offsetPosition, animationDuration);
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _elapsedTime = 0;
        _animator.enabled = true;
    }
    
    public void DisableAnimator()
    {
        _animator.enabled = false;
    }
    
}
