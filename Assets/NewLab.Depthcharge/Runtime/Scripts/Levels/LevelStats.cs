using System;

namespace Depthcharge.LevelManagement
{

    public class LevelStats
    {

        public int EnemiesSpawned { get; private set; }
        public int ActiveEnemies { get; private set; }
        public int EnemiesDefeated { get; private set; }
        public float Score { get; private set; }
        public int EnemiesMissed
        {
            get => EnemiesSpawned - EnemiesDefeated - ActiveEnemies;
        }
        public TimeSpan Time { get; private set; }
        public Action<float> OnIncreaseScore = null;

        public LevelStats()
        {
            EnemiesSpawned = 0;
            ActiveEnemies = 0;
            EnemiesDefeated = 0;
            Score = 0;
        }

        public float IncreaseScore(float points)
        {
            Score += points;
            OnIncreaseScore?.Invoke(Score);
            return Score;
        }

        public int IncreaseEnemiesSpawned()
        {
            EnemiesSpawned++;
            return EnemiesSpawned;
        }
        public int IncreaseEnemiesDefeated()
        {
            EnemiesDefeated++;
            return EnemiesDefeated;
        }

        public int IncreaseActiveEnemies()
        {
            ActiveEnemies++;
            return ActiveEnemies;
        }
        public int DecreaseActiveEnemies()
        {
            ActiveEnemies--;
            return ActiveEnemies;
        }

        public TimeSpan IncreaseTime(float elapsedTime)
        {
            Time = TimeSpan.FromSeconds(elapsedTime);
            return Time;
        }

    }

}