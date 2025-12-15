using Zenject;

public class GameOverScreen : Screen
{
    [Inject]
    private void Construct(SignalBus signalBus)
    {
        SignalBus = signalBus;    
    } 
    
    protected override void OnButtonClick()
    {
        Close();
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        button.interactable = true;
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        button.interactable = false;
        SignalBus.Fire(new GameStateChangedSignal(GameState.Starting));
    }
}
