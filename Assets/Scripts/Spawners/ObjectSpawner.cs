using UnityEngine;

public class ObjectSpawner
{
    protected SpawnerSettings Settings;

    protected ObjectSpawner(SpawnerSettings settings)
    {
        Settings = settings;
    }

    public bool IsTimeToSpawn(float elapsedTime)
    {
        return elapsedTime > Settings.CurrentSecondsBetweenSpawn;
    }

    public void SpawnObject(GameObject obj, Vector3 spawnPosition)
    {
        var spawnPositionY = Random.Range(Settings.MinSpawnPositionY, Settings.MaxSpawnPositionY);
        var spawnPoint = new Vector3(spawnPosition.x, spawnPositionY, spawnPosition.z);
        obj.SetActive(true);
        obj.transform.position = spawnPoint;
    }
}
