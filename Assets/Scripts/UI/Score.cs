using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private Bird bird;
    [SerializeField] private TMP_Text score;

    private void OnEnable()
    {
        bird.UpdateScore += UpdateScore;    
    }

    private void OnDisable()
    {
        bird.UpdateScore -= UpdateScore;    
    }

    private void UpdateScore(int playerScore)
    {
        score.text = playerScore.ToString();    
    }
    
}
