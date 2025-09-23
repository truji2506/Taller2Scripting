using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public event Action<int> OnScoreChanged;
    public event Action<int> OnClicksChanged;

    private int _score;
    private int _clicks;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    public int Clicks
    {
        get => _clicks;
        private set
        {
            _clicks = value;
            OnClicksChanged?.Invoke(_clicks);
        }
    }

    public void AddPoints(int amount)
    {
        Score += amount;
        Clicks += 1;
    }

    public void ApplyEffect(IEffect effect)
    {
        Score = effect.ApplyEffect(Score);
    }
}
