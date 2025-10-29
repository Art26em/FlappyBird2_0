using UnityEngine;
using Zenject;

public class PipeGenerator : ObjectPool
{
    private ObjectSpawner _objectSpawner;
    private ObjectMover _objectMover;
    
    private float _elapsedTime;

    [Inject]
    private void Construct(ObjectSpawner objectSpawner, ObjectMover objectMover)
    {
        _objectSpawner = objectSpawner;
        _objectMover = objectMover;
    }
    
    private void Start()
    {
        Initialize();        
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!_objectSpawner.IsTimeToSpawn(_elapsedTime)) return;

        if (TryGetObject(out var pipe))
        {
            _objectSpawner.SpawnObject(pipe, transform.position);
            _objectMover.StartObjectMoving(pipe);
        }
        DisableObjectAbroadScreen();
        _elapsedTime = 0;

    }
    
}
