using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class ObjectSpawner
{
    private readonly float _secondsBetweenSpawn;
    private readonly float _maxSpawnPositionY; 
    private readonly float _minSpawnPositionY;

    public ObjectSpawner(float minSpawnPositionY, float maxSpawnPositionY, float secondsBetweenSpawn)
    {
        _minSpawnPositionY = minSpawnPositionY;
        _maxSpawnPositionY = maxSpawnPositionY;
        _secondsBetweenSpawn = secondsBetweenSpawn;
    }

    public bool IsTimeToSpawn(float elapsedTime)
    {
        return elapsedTime >= _secondsBetweenSpawn;
    }
    
    public void SpawnObject(GameObject obj, Vector3 spawnPosition)
    {
        var spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
        var spawnPoint = new Vector3(spawnPosition.x, spawnPositionY, spawnPosition.z);
        obj.SetActive(true);
        obj.transform.position = spawnPoint;
    }
}
