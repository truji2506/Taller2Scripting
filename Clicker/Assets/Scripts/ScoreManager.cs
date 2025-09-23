using System;
using UnityEngine;

/// <summary>
/// Maneja la puntuación y los clics del jugador.
/// Implementa el patrón Singleton para acceso global.
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    // 🔔 Eventos para notificar cambios en score y clics
    public event Action<int> OnScoreChanged;
    public event Action<int> OnClicksChanged;

    // 🔢 Campos privados
    private int _score;
    private int _clicks;

    /// <summary>
    /// Puntaje actual del jugador.
    /// Al actualizarlo dispara el evento OnScoreChanged.
    /// </summary>
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    /// <summary>
    /// Número de clics realizados.
    /// Al actualizarlo dispara el evento OnClicksChanged.
    /// </summary>
    public int Clicks
    {
        get => _clicks;
        private set
        {
            _clicks = value;
            OnClicksChanged?.Invoke(_clicks);
        }
    }

    /// <summary>
    /// Suma puntos al score y aumenta el contador de clics.
    /// </summary>
    /// <param name="amount">Cantidad de puntos a sumar.</param>
    public void AddPoints(int amount)
    {
        Score += amount;
        Clicks++;
    }

    /// <summary>
    /// Aplica un efecto (ejemplo: PowerUp o penalización).
    /// </summary>
    /// <param name="effect">Estrategia que implementa IEffect.</param>
    public void ApplyEffect(IEffect effect)
    {
        if (effect == null)
        {
            Debug.LogWarning("⚠️ Se intentó aplicar un efecto nulo en ScoreManager.");
            return;
        }

        Score = effect.ApplyEffect(Score);
    }
}
