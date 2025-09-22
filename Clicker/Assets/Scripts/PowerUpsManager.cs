using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Este método aplicará el power-up que duplica los puntos.
    // Lo llamaremos desde el OnClick del botón en el editor de Unity.
    public void ApplyDoublePoints()
    {
        // 1. Crea una instancia de la estrategia del power-up
        DoublePointsPowerUp effect = new DoublePointsPowerUp();

        // 2. Llama al ScoreManager para que aplique el efecto
        ScoreManager.Instance.ApplyEffect(effect);

        // Opcional: Puedes añadir un log para confirmar que se aplicó
        Debug.Log("Power-Up Applied: Points Doubled!");
    }

    // Este método podría ser para otro botón de penalización.
    public void ApplyResetPenalty()
    {
        // 1. Crea una instancia de la estrategia de penalización
        ResetScorePenalty effect = new ResetScorePenalty();

        // 2. Llama al ScoreManager para que aplique el efecto
        ScoreManager.Instance.ApplyEffect(effect);

        Debug.Log("Penalty Applied: Score Reset!");
    }
}
