
public class SpawnerSettings
{
    public float MinSpawnPositionY { get; private set; }
    public float MaxSpawnPositionY { get; private set; }
    public float StartSecondsBetweenSpawn { get; private set; }
    public float LevelUpDecreasingSpawnSeconds { get; private set; }
    public float MinSecondsBetweenSpawn {get; private set;}
    public float CurrentSecondsBetweenSpawn;

    public SpawnerSettings(
        float minSpawnPositionY, 
        float maxSpawnPositionY, 
        float startSecondsBetweenSpawn,
        float levelUpDecreasingSpawnSeconds = 0,
        float minSecondsBetweenSpawn = 0)
    {
        MinSpawnPositionY = minSpawnPositionY;
        MaxSpawnPositionY = maxSpawnPositionY;
        StartSecondsBetweenSpawn = startSecondsBetweenSpawn;
        CurrentSecondsBetweenSpawn = startSecondsBetweenSpawn;
        LevelUpDecreasingSpawnSeconds = levelUpDecreasingSpawnSeconds;
        MinSecondsBetweenSpawn = minSecondsBetweenSpawn;
    }
}
