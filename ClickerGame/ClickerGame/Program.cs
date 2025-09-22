using System;
using System.Threading;

namespace ClickerConsole
{
    // -------------------------
    // PATTERN: Strategy
    // -------------------------
    public interface IScoreStrategy
    {
        int CalculatePoints(int basePoints);
    }

    public class NormalScore : IScoreStrategy
    {
        public int CalculatePoints(int basePoints) => basePoints;
    }

    public class DoubleScore : IScoreStrategy
    {
        public int CalculatePoints(int basePoints) => basePoints * 2;
    }

    public class RandomBonusScore : IScoreStrategy
    {
        private Random _rnd = new Random();
        public int CalculatePoints(int basePoints) => basePoints + _rnd.Next(0, 4);
    }

    // -------------------------
    // PATTERN: Singleton + Observer (events)
    // -------------------------
    public sealed class GameManager
    {
        private static readonly Lazy<GameManager> _lazy = new Lazy<GameManager>(() => new GameManager());
        public static GameManager Instance => _lazy.Value;

        private long _score;



        private int _pointsPerSecond;
        private Timer _ppsTimer;

        // Eventos (Observer con delegados)
        public delegate void ScoreChangedHandler(long newScore);
        public event ScoreChangedHandler OnScoreChanged;

        public event Action<string> OnNotification;
        public event Action<string> OnPowerUpActivated;

        private GameManager()
        {
            _score = 0;
            _pointsPerSecond = 0;

            // Timer de puntos por segundo (cada 1s)
            _ppsTimer = new Timer(_ =>
            {
                if (_pointsPerSecond > 0)
                    AddScore(_pointsPerSecond);
            }, null, 1000, 1000);
        }

        public long Score
        {

            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        public int PointsPerSecond => _pointsPerSecond;

        public void AddScore(int amount)
        {
            if (amount > 0)
                Score += amount;
        }

        public bool TrySpend(long cost)
        {
            if (Score >= cost)
            {
                Score -= cost;
                return true;
            }
            return false;
        }

        public void IncreasePPS(int amount)
        {
            _pointsPerSecond += amount;
            OnNotification?.Invoke($"PPS actualizado: {_pointsPerSecond}");
        }

        public void NotifyPowerUp(string msg)
        {
            OnPowerUpActivated?.Invoke(msg);
        }

        public void Notify(string msg)
        {
            OnNotification?.Invoke(msg);
        }
    }

    // -------------------------
    // Objeto clickeable (usa Strategy)
    // -------------------------
    public class ClickableCrystal
    {
        public IScoreStrategy ScoreStrategy { get; set; } = new NormalScore();

        public void Click()
        {
            int pts = ScoreStrategy.CalculatePoints(1);
            GameManager.Instance.AddScore(pts);
            GameManager.Instance.Notify($"Click: +{pts}");
        }
    }

    
