using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Principio Abierto/Cerrado: Abierto a extender con más efectos
public interface IEffect
{
    int ApplyEffect(int currentScore);
}

// Implementación 1: Power-up
public class DoublePointsPowerUp : IEffect
{
    public int ApplyEffect(int currentScore)
    {
        return currentScore + 2;
    }
}

// Implementación 2: Penalización
public class ResetScorePenalty : IEffect
{
    public int ApplyEffect(int currentScore)
    {
        return 0;
    }
}