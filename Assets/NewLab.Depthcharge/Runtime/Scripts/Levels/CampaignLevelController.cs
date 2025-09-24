using Depthcharge.Actors;
using Depthcharge.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    public class CampaignLevelController : BaseLevelController
    {

        private int levelNumber = 0;
        private List<EnemySpawner> spawners = null;
        private EnemyListenersContainer listeners = null;
        [SerializeField]
        private EnemySpawnerProvider[] spawnerProviders = null;

        protected override void SetUp()
        {
            levelNumber = gameLogic.IncreaseCurrentLevelNumber();
            base.SetUp();
            spawners = LevelControllerConfigurator.SetEnemySpawners(Configuration, ref spawnerProviders);
            listeners = new EnemyListenersContainer(OnSpawnEnemy, OnDefeatEnemy, OnDeactivateEnemy);
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.CampaignUI;
        }
        protected override void SetGameUIContext(ref GameUIContext context)
        {
            base.SetGameUIContext(ref context);
            context.levelNumber = levelNumber;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            foreach (EnemySpawner spawner in spawners) 
                spawner.CleanUp();
            systemsRoot.UISystem.SetCampaignUIActiveness(false);
        }

        #region Events

        protected override void AddListeners()
        {
            base.AddListeners();
            LevelControllerConfigurator.AddEnemiesListeners(spawners, listeners);
        }
        protected override void RemoveListeners()
        {
            base.RemoveListeners();
            LevelControllerConfigurator.RemoveEnemiesListeners(spawners, listeners);
        }

        private void OnSpawnEnemy()
        {
            _stats.IncreaseEnemiesSpawned();
            _stats.IncreaseActiveEnemies();
        }

        private void OnDeactivateEnemy(EnemyController enemy)
        {
            _stats.DecreaseActiveEnemies();
        }
        private void OnDefeatEnemy(EnemyController enemy)
        {
            _stats.IncreaseEnemiesDefeated();
            _stats.IncreaseScore(enemy.ScorePoints);
            UI.SetScoreText(_stats.Score.ToString());
            UI.SetEnemiesText(_stats.EnemiesDefeated.ToString());
            if (_winCondition.Strategy.WinLevelCondition(this))
            {
                _stats.DecreaseActiveEnemies();
                OnWin?.Invoke();
                Debug.Log($"Enemies missed: {Stats.EnemiesMissed}");
            }
        }

        #endregion

    }

}