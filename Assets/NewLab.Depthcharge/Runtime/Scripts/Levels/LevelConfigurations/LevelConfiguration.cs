using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [CreateAssetMenu(menuName = "Scriptable Objects/LevelManagement/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {

        public enum LevelDifficulty : byte { Easy, Normal, Hard, Random }

        [SerializeField]
        private LevelDifficulty _difficulty = LevelDifficulty.Easy;
        public LevelDifficulty Difficulty { get => _difficulty; }

        [SerializeField]
        private List<WinConditionContainer> _winCondition = null;
        public List<WinConditionContainer> WinCondition { get => _winCondition; }

        [SerializeField]
        private float _minEnemySpawnDelay = 1.0f;
        public float MinEnemySpawnDelay { get => _minEnemySpawnDelay; }

        [SerializeField]
        private float _maxEnemySpawnDelay = 1.0f;
        public float MaxEnemySpawnDelay { get => _maxEnemySpawnDelay; }

    }

}