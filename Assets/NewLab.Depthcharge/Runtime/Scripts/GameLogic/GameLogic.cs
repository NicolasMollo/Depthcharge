using Depthcharge.LevelManagement;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance { get; private set; } = null;

        #region Level configurations

        [SerializeField]
        private LevelConfiguration easyConfiguration = null;
        [SerializeField]
        private LevelConfiguration normalConfiguration = null;
        [SerializeField]
        private LevelConfiguration hardConfiguration = null;
        [SerializeField]
        private LevelConfiguration randomConfiguration = null;

        #endregion

        [SerializeField]
        [Range(1, 10)]
        private int levelsPerDifficulty = 5;

        [SerializeField]
        private int _currentLevelNumber = 0;
        public int CurrentLevelNumber { get => _currentLevelNumber; }
        public bool IsBossLevel { get => _currentLevelNumber % levelsPerDifficulty == 0; }
        public bool IsLastLevel { get => _currentLevelNumber == levelsPerDifficulty * 3; }
        public bool LoadGameTransitionsState { get; set; }

        private void Awake()
        {
            SetSingleton();
            DontDestroyOnLoad(this.gameObject);
            LoadGameTransitionsState = true;
            // IncreaseCurrentLevelNumber();

            _currentLevelNumber = levelsPerDifficulty * 3;
        }
        private void SetSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        #region API

        public int IncreaseCurrentLevelNumber()
        {
            _currentLevelNumber++;
            return _currentLevelNumber;
        }
        public int DecreaseCurrentLevelNumber()
        {
            _currentLevelNumber--;
            return _currentLevelNumber;
        }
        public void ResetCurrentLevelNumber()
        {
            _currentLevelNumber = 1;
        }

        public LevelConfiguration GetLevelConfiguration()
        {
            int easyLevelsPerDifficulty = levelsPerDifficulty;
            int normalLevelsPerDifficulty = levelsPerDifficulty * 2;
            int hardLevelsPerDifficulty = levelsPerDifficulty * 3;

            if (_currentLevelNumber >= 0 && _currentLevelNumber <= easyLevelsPerDifficulty)
                return easyConfiguration;
            else if (_currentLevelNumber > easyLevelsPerDifficulty && _currentLevelNumber <= normalLevelsPerDifficulty)
                return normalConfiguration;
            else if (_currentLevelNumber > normalLevelsPerDifficulty && _currentLevelNumber <= hardLevelsPerDifficulty)
                return hardConfiguration;
            else return randomConfiguration;
        }

        public LevelConfiguration GetSurvivalConfiguration()
        {
            return randomConfiguration;
        }

        #endregion

    }

}