using Depthcharge.LevelManagement;
using System;
using UnityEngine;

namespace Depthcharge.GameManagement
{

    [Serializable]
    public class LevelConfigurationService
    {

        [SerializeField]
        private LevelConfiguration _easyConfiguration = null;
        [SerializeField]
        private LevelConfiguration _normalConfiguration = null;
        [SerializeField]
        private LevelConfiguration _hardConfiguration = null;
        [SerializeField]
        private LevelConfiguration _randomConfiguration = null;


        internal LevelConfiguration GetCampaignLevelConfiguration(int levelsPerTier, int currentLevelNumber)
        {
            int easyLevelsPerTier = levelsPerTier;
            int normalLevelsPerTier = levelsPerTier * 2;
            int hardLevelsPerTier = levelsPerTier * 3;

            if (currentLevelNumber >= 0 && currentLevelNumber <= easyLevelsPerTier) 
                return _easyConfiguration;
            else if (currentLevelNumber > easyLevelsPerTier && currentLevelNumber <= normalLevelsPerTier) 
                return _normalConfiguration;
            else if (currentLevelNumber > normalLevelsPerTier && currentLevelNumber <= hardLevelsPerTier) 
                return _hardConfiguration;
            else return _randomConfiguration;
        }

        internal LevelConfiguration GetSurvivalConfiguration()
        {
            return _randomConfiguration;
        }

    }

}