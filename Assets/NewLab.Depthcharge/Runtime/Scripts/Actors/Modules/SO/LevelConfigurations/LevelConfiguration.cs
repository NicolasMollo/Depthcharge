using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {

        public enum LevelDifficulty : byte { Easy, Medium, Hard, Random }

        [SerializeField]
        private LevelDifficulty _difficulty = LevelDifficulty.Easy;
        public LevelDifficulty Difficulty { get => _difficulty; }

        [SerializeField]
        private float _minEnemySpawnDelay = 1.0f;
        public float MinEnemySpawnDelay { get => _minEnemySpawnDelay; }

        [SerializeField]
        private float _maxEnemySpawnDelay = 1.0f;
        public float MaxEnemySpawnDelay { get => _maxEnemySpawnDelay; }

        [SerializeField]
        private int _enemyToDefeat = 10;
        public int EnemyToDefeat { get => _enemyToDefeat; }

        [SerializeField]
        private int _scoreToReach = 100;
        public int ScoreToReach { get => _scoreToReach; }

    }

}