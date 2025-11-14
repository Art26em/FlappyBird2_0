using TMPro;
using UnityEngine;
using DG.Tweening;

public class StartScreen : Screen
{
    [SerializeField] private TMP_Text scoreText;

    private const string StartScoreText = "0";

    protected override void OnButtonClick()
    {
        Close();
    }

    public override void Open()
    {
        button.interactable = true;
        scoreText.text = "";
    }

    public override void Close()
    {
        button.interactable = false;
        DOTween.To(FadeOut, 1f, 0f, 2f);
        scoreText.text = StartScoreText;
        SignalBus.Fire(new GameStateChangedSignal(GameState.Starting));
    }

    private void FadeOut(float value)
    {
        canvasGroup.alpha = value;
        if (value == 0)
            gameObject.SetActive(false);
    }
    
}
