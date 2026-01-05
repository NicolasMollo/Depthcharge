using Depthcharge.LevelManagement;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [DisallowMultipleComponent]
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance { get; private set; } = null;

        [SerializeField]
        private LevelConfigurationService _levelConfigurator = null;
        [SerializeField]
        private GameProgression _progression = null;

        public LevelConfigurationService LevelConfigurator { get => _levelConfigurator; }
        public GameProgression GameProgression { get => _progression; }
        public bool LoadGameTransitionsState { get; set; }


        private void Awake()
        {
            SetSingleton();
            LoadGameTransitionsState = true;
            _progression.ResetCurrentLevelNumber();
            DontDestroyOnLoad(this.gameObject);
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


        public LevelConfiguration GetCampaignConfiguration()
        {
            return _levelConfigurator.GetCampaignLevelConfiguration(
                _progression.LevelPerTier, 
                _progression.CurrentLevelNumber
                );
        }
        public LevelConfiguration GetSurvivalConfiguration()
        {
            return _levelConfigurator.GetSurvivalConfiguration();
        }

        public void IncreaseLevelNumber()
        {
            _progression.IncreaseCurrentLevelNumber();
        }
        public void DecreaseLevelNumber()
        {
            _progression.DecreaseCurrentLevelNumber();
        }
        public void ResetLevelNumber()
        {
            _progression.ResetCurrentLevelNumber();
        }

    }

}