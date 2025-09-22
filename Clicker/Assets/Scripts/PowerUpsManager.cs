using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Este m�todo aplicar� el power-up que duplica los puntos.
    // Lo llamaremos desde el OnClick del bot�n en el editor de Unity.
    public void ApplyDoublePoints()
    {
        // 1. Crea una instancia de la estrategia del power-up
        DoublePointsPowerUp effect = new DoublePointsPowerUp();

        // 2. Llama al ScoreManager para que aplique el efecto
        ScoreManager.Instance.ApplyEffect(effect);

        // Opcional: Puedes a�adir un log para confirmar que se aplic�
        Debug.Log("Power-Up Applied: Points Doubled!");
    }

    // Este m�todo podr�a ser para otro bot�n de penalizaci�n.
    public void ApplyResetPenalty()
    {
        // 1. Crea una instancia de la estrategia de penalizaci�n
        ResetScorePenalty effect = new ResetScorePenalty();

        // 2. Llama al ScoreManager para que aplique el efecto
        ScoreManager.Instance.ApplyEffect(effect);

        Debug.Log("Penalty Applied: Score Reset!");
    }
}
