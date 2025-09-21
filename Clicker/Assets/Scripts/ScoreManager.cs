using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ScoreManager : Singleton<ScoreManager>
{
    public event Action<int> OnScoreChanged;
    private int _score;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public void AddPoints(int amount)
    {
        Score += amount;
    }

    public void ApplyEffect(IEffect effect)
    {
        Score = effect.ApplyEffect(Score);
    }
}