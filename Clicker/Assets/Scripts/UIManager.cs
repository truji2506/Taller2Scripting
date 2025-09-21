using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // Necesario para TextMeshPro

public class UIManager : MonoBehaviour
{
    // 1. Un espacio público para arrastrar nuestro texto
    public TextMeshProUGUI scoreText;

    // 2. Cuando este objeto se activa, se suscribe a los cambios.
    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
    }

    // 3. Cuando se desactiva, se desuscribe para evitar errores.
    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
    }

    // 4. Este método es el que reacciona al evento.
    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}