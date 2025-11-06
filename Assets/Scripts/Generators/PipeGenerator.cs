using Zenject;

public class PipeGenerator : ObjectGenerator
{
    [Inject]
    private void Construct(PipeSpawner pipeSpawner, PipeMover pipeMover)
    {
        ObjectSpawner = pipeSpawner;
        ObjectMover = pipeMover;
    }
    
}
