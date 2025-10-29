using UnityEngine;
using Zenject;

public class Bootstrap : MonoInstaller
{
    [SerializeField] private Bird bird;
    
    [Header("ObjectSpawner settings")]
    [SerializeField] private float secondsBetweenSpawn;
    [SerializeField] private float maxSpawnPositionY; 
    [SerializeField] private float minSpawnPositionY;
    
    [Header("ObjectMover settings")]
    [SerializeField] private float moveSpeed = 2; 
    
    public override void InstallBindings()
    {
        Container.Bind<Bird>().
            FromInstance(bird).AsSingle();

        Container.Bind<ObjectSpawner>().
            FromInstance(new ObjectSpawner(minSpawnPositionY, maxSpawnPositionY, secondsBetweenSpawn)).
            AsTransient();
        
        Container.Bind<ObjectMover>().
 			FromNewComponentOnNewGameObject().
            AsTransient().
            OnInstantiated<ObjectMover>((context, mover) => 
            {
                mover.Initialize(moveSpeed);
            });
    }

}