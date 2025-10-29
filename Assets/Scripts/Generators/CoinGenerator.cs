using UnityEngine;
using Zenject;

public class CoinGenerator : ObjectPool
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
