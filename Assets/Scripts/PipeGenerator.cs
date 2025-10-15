using UnityEngine;
using Random = UnityEngine.Random;

public class PipeGenerator : ObjectPool
{
    [SerializeField] private GameObject template;
    [SerializeField] private float secondsBetweenSpawn;
    [SerializeField] private float maxSpawnPositionY; 
    [SerializeField] private float minSpawnPositionY; 
    
    private float _elapsedTime;
    
    private void Start()
    {
        Initialize(template);        
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > secondsBetweenSpawn)
        {
            if (TryGetObject(out GameObject pipe))
            {
                _elapsedTime = 0;
                float spawnPositionY = Random.Range(minSpawnPositionY, maxSpawnPositionY);
                Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);
                
                pipe.SetActive(true);
                pipe.transform.position = spawnPoint;
                DisableObjectAbroadScreen();
            }   
        }
    }

	
    
}
