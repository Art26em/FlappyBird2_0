using UnityEngine;

public class MovementSettings
{
    public float TapForce {get; private set;}
    public float RotationSpeed {get; private set;}
    public float MaxRotationZ {get; private set;}
    public float MinRotationZ {get; private set;}
    public Quaternion MinRotation {get; private set;}
    public Quaternion MaxRotation {get; private set;}
    public Vector3 StartPosition {get; private set;}
    public Vector3 OffsetPosition {get; private set;}
    
    public MovementSettings(
        float tapForce,
        float rotationSpeed,
        float maxRotationZ,
        float minRotationZ,
        Vector3 offsetPosition)
    {
        TapForce = tapForce;
        RotationSpeed = rotationSpeed;
        MaxRotationZ = maxRotationZ;
        MinRotationZ = minRotationZ;
        MinRotation = Quaternion.Euler(0, 0, MinRotationZ);
        MaxRotation = Quaternion.Euler(0, 0, MaxRotationZ);
        OffsetPosition = offsetPosition;
    }
    
}