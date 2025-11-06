using UnityEngine.Events;

public class GameOverScreen : Screen
{
    protected override void OnButtonClick()
    {
        Close();
    }

    public override void Open()
    {
        canvasGroup.alpha = 1;
        button.interactable = true;
    }

    public override void Close()
    {
        canvasGroup.alpha = 0;
        button.interactable = false;
        SignalBus.Fire(new GameStateChangedSignal(GameState.Starting));
    }
}
