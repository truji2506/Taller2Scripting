using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para Button
using TMPro; // Necesario para TextMeshPro

public class UIManager : MonoBehaviour
{
    // 1. Un espacio público para arrastrar nuestro texto
    public TextMeshProUGUI scoreText;
    public Button powerUpButton; // Arrastra aquí tu nuevo botón de Power-Up

    // 2. Puntuación necesaria para desbloquear el botón
    private const int SCORE_TO_UNLOCK_POWERUP = 50;

    void Start()
    {
        // 3. Asegúrate de que el botón esté desactivado al empezar
        powerUpButton.interactable = false;
    }

    // Se suscribe a los cambios de puntuación
    private void OnEnable()
    {
        // Verificamos que exista una instancia para evitar errores al iniciar
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            // Actualiza el estado inicial en caso de que ya haya una puntuación guardada
            UpdateScoreText(ScoreManager.Instance.Score);
        }
    }

    // Se desuscribe para evitar errores
    private void OnDisable()
    {
        // Verificamos que la instancia no haya sido destruida (al cerrar el juego)
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
        }
    }

    // Este método reacciona al evento de cambio de puntuación
    private void UpdateScoreText(int newScore)
    {
        scoreText.text = "Score: " + newScore;

        // 4. Lógica para desbloquear el botón
        // Si la puntuación actual es mayor o igual a la necesaria, activa el botón
        if (newScore >= SCORE_TO_UNLOCK_POWERUP)
        {
            powerUpButton.interactable = true;
        }
    }
}