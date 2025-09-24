using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button powerUpButton; 

    private const int SCORE_TO_UNLOCK_POWERUP = 50;

    void Start()
    {
        if (powerUpButton != null)
            powerUpButton.interactable = false;
    }

    // Usamos OnEnable y OnDisable para suscribirnos y desuscribirnos de eventos
    // UIManager se suscribe a los eventos de ScoreManager
    private void OnEnable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance.Score);
        }
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText; // -= Evita referencias colgantes cuando el objeto se destruye        
        }
    }

    private void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + newScore;
        else
            Debug.LogError("❌ ScoreText no está asignado en el UIManager.");

        if (powerUpButton != null && newScore >= SCORE_TO_UNLOCK_POWERUP)
            powerUpButton.interactable = true;
    }
}