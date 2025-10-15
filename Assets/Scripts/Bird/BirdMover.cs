using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMover : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float tapForce;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float minRotationZ;
    
    private Rigidbody2D _rigidbody;
    private Quaternion _minRotation;
    private Quaternion _maxRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _minRotation = Quaternion.Euler(0, 0, minRotationZ);
        _maxRotation = Quaternion.Euler(0, 0, maxRotationZ);
        ResetBird();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.rotation = _maxRotation;
            _rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Impulse);
        }  
        
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, Time.deltaTime * rotationSpeed);
        
    }

    public void ResetBird()
    {
        transform.position = startPosition;
        _rigidbody.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
}
