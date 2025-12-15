using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Button button;

    protected SignalBus SignalBus;
    
    private void OnEnable()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();

    public abstract void Open();
    
    public abstract void Close();

}
