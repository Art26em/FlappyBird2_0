using UnityEngine;
using Zenject;

public class GameResumeController : GameStartController
{
    [Inject]
    private void Construct(PipeGenerator pipeGenerator, CoinGenerator coinGenerator, Bird bird)
    {
        PipeGenerator = pipeGenerator;
        CoinGenerator = coinGenerator;
        Bird = bird;
    }
    
    public void ResumeGame()
    {
        ResetGenerators();
        Time.timeScale = 1;
        Bird.ResetPlayer(false);
    }  
}