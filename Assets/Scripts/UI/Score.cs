using TMPro;
using UnityEngine;
using Zenject;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text score;
    
    private Bird _bird;
    
    [Inject]
    private void Construct(Bird bird)
    {
        _bird = bird;
    }
    
    private void OnEnable()
    {
        _bird.UpdateScore += UpdateScore;    
    }

    private void OnDisable()
    {
        _bird.UpdateScore -= UpdateScore;    
    }

    private void UpdateScore(int playerScore)
    {
        score.text = playerScore.ToString();    
    }
    
}
