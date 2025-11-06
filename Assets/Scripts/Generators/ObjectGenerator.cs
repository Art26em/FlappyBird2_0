using UnityEngine;

public class ObjectGenerator : ObjectPool
{
    protected ObjectSpawner ObjectSpawner;
    protected ObjectMover ObjectMover;

    private float _elapsedTime;
    
    private void Start()
    {
        Initialize();        
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!ObjectSpawner.IsTimeToSpawn(_elapsedTime)) return;

        if (TryGetObject(out var obj))
        {
            ObjectSpawner.SpawnObject(obj, transform.position);
            ObjectMover.StartObjectMoving(obj);
        }
        DisableObjectAbroadScreen();
        _elapsedTime = 0;
    }        
}