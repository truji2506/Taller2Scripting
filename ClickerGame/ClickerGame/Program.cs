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

    
