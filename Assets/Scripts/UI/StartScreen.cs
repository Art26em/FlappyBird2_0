using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StartScreen : Screen
{
    [SerializeField] private TMP_Text scoreText;
    public event UnityAction PlayButtonClick;
    
    protected override void OnButtonClick()
    {
        PlayButtonClick?.Invoke();
    }

    public override void Open()
    {
        canvasGroup.alpha = 1;
        button.interactable = true;
        scoreText.text = "";
    }

    public override void Close()
    {
        canvasGroup.alpha = 0;
        button.interactable = false;
        scoreText.text = "0";
    }
}
