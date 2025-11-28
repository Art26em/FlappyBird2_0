using UnityEngine;
using Zenject;

public class GameStartController : GameStateController
{
    
    [Inject]
    private void Construct(PipeGenerator pipeGenerator, CoinGenerator coinGenerator, Bird bird)
    {
        PipeGenerator = pipeGenerator;
        CoinGenerator = coinGenerator;
        Bird = bird;
    }
    
    public void StartGame()
    {
        Time.timeScale = 1;
        ResetGenerators();
        Bird.ResetPlayer();
    }
}
