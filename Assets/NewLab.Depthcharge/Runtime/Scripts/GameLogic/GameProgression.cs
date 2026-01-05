using System;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [Serializable]
    public class GameProgression
    {

        [SerializeField]
        [Range(1, 10)]
        private int _levelsPerDifficulty = 3;

        private int _currentLevelNumber = 0;

        public int CurrentLevelNumber { get => _currentLevelNumber; }
        public bool IsBossLevel { get => _currentLevelNumber % _levelsPerDifficulty == 0; }
        public bool PreviousLevelWasBoss { get; set; }
        public bool IsLastLevel { get => _currentLevelNumber == _levelsPerDifficulty * 3; }
        public int LevelPerTier { get => _levelsPerDifficulty; }


        internal void IncreaseCurrentLevelNumber()
        {
            _currentLevelNumber++;
        }
        internal void DecreaseCurrentLevelNumber()
        {
            _currentLevelNumber--;
        }
        internal void ResetCurrentLevelNumber()
        {
            _currentLevelNumber = 1;

            // _currentLevelNumber = _levelsPerDifficulty + 2;
            // _currentLevelNumber = _levelsPerDifficulty * 2;
            // _currentLevelNumber = _levelsPerDifficulty * 3;
        }

    }

}