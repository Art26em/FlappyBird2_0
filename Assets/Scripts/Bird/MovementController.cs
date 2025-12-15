using DG.Tweening;
using UnityEngine;

public class MovementController
{
    private Transform _player;
    private Rigidbody2D _rigidbody;
    private readonly MovementSettings _movementSettings;

    public MovementController(MovementSettings movementSettings)
    {
        _movementSettings = movementSettings;
    }
    
    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    public void SetRigidbody(Rigidbody2D rigidbody)
    {
        _rigidbody = rigidbody;
    }
    
    public bool TryMove()
    {
        if (!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0)) return false;
        _player.rotation = _movementSettings.MaxRotation;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector2.up * _movementSettings.TapForce, ForceMode2D.Force);
        return true;
    }

    public void Rotate()
    {
        _player.rotation = Quaternion.Lerp(
            _player.rotation, 
            _movementSettings.MinRotation, 
            Time.deltaTime * _movementSettings.RotationSpeed);
    }

    public void ResetPlayer(float startAnimationDuration)
    {
        _player.DOMove(_movementSettings.OffsetPosition, startAnimationDuration);
        ResetVelocity();
        ResetRotation();
    }
    
    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector2.zero;
    }
 
    public void ResetRotation()
    {
        _player.rotation = Quaternion.Euler(0, 0, 0);
    }
    
}