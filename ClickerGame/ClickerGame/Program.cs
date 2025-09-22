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

    // -------------------------
    // PowerUpManager (usa Timer con delegados)
    // -------------------------
    public class PowerUpManager
    {
        private readonly ClickableCrystal _crystal;
        private Timer _doubleTimer;
        private bool _doubleActive = false;

        public PowerUpManager(ClickableCrystal crystal)
        {
            _crystal = crystal;
        }

        public void BuyDoublePoints()
        {
            const long cost = 50;
            if (!GameManager.Instance.TrySpend(cost))
            {
                GameManager.Instance.Notify("No tienes suficientes puntos para Double (50).");
                return;
            }

            ActivateDoublePoints(20000, () =>
            {
                GameManager.Instance.Notify("Double Points finalizó.");
            });
            GameManager.Instance.Notify("Double Points activado por 20s.");
        }

        private void ActivateDoublePoints(int durationMs, Action onComplete)
        {
            if (_doubleActive)
            {
                GameManager.Instance.Notify("Double ya estaba activo. Reiniciando duración...");
                _doubleTimer?.Dispose();
            }
            else
            {
                _crystal.ScoreStrategy = new DoubleScore();
                _doubleActive = true;
                GameManager.Instance.Notify("Estrategia DoubleScore activada.");
                GameManager.Instance.NotifyPowerUp("Double Activated");
            }

            _doubleTimer = new Timer(_ =>
            {
                _crystal.ScoreStrategy = new NormalScore();
                _doubleActive = false;
                onComplete?.Invoke();
            }, null, durationMs, Timeout.Infinite);
        }

        public void BuyPPS()
        {
            const long cost = 30;
            if (!GameManager.Instance.TrySpend(cost))
            {
                GameManager.Instance.Notify("No tienes suficientes puntos para PPS (+1).");
                return;
            }

            GameManager.Instance.IncreasePPS(1);
            GameManager.Instance.Notify("Has comprado +1 PPS (permanente).");
            GameManager.Instance.NotifyPowerUp("PPS Bought");
        }
    }

    
