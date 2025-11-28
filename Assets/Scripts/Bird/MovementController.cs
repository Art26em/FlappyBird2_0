using UnityEngine;

public class MovementController
{
    private float _tapForce;
    private float _rotationSpeed;
    private float _maxRotationZ;
    private float _minRotationZ;
    
    private readonly Transform _player;
    private readonly Rigidbody2D _rigidbody;
    private readonly Quaternion _minRotation;
    private readonly Quaternion _maxRotation;

    public MovementController(Transform player, Rigidbody2D rigidbody)
    {
        _player = player;
        _rigidbody = rigidbody;
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
    }
    
    public void Move()
    {
        _player.rotation = _maxRotation;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector2.up * _tapForce, ForceMode2D.Force);
    }

    public void Rotate()
    {
        _player.rotation = Quaternion.Lerp(_player.rotation, _minRotation, Time.deltaTime * _rotationSpeed);
    }
    
}