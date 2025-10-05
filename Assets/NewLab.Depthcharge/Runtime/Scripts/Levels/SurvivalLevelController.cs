using Depthcharge.Actors;
using Depthcharge.Events;
using Depthcharge.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public class SurvivalLevelController : BaseLevelController
    {

        private float elapsedTime = 0;
        private SurvivalUIController survivalUI;
        private EnemyListenersContainer listeners = null;

        private List<EnemySpawner> spawners = null;
        [SerializeField]
        private EnemySpawnerProvider[] spawnerProviders = null;

        protected override void InternalSetUp()
        {
            base.InternalSetUp();
            survivalUI = this.UI as SurvivalUIController;
            if (survivalUI == null)
            {
                Debug.LogError($"=== {this.gameObject.name} === UI type is wrong!");
                return;
            }
            spawners = LevelControllerConfigurator.SetEnemySpawners(_configuration, ref spawnerProviders);
            listeners = new EnemyListenersContainer(OnSpawnEnemy, OnDefeatEnemy, OnDeactivateEnemy);
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.SurvivalUI;
            systemsRoot.UISystem.SetCurrentGameUI(UI);
        }
        protected override void InternalCleanUp()
        {
            base.InternalCleanUp();
            foreach (EnemySpawner spawner in spawners)
                spawner.CleanUp();
            systemsRoot.UISystem.SetSurvivalUIActiveness(false);
        }

        protected override void AddListeners()
        {
            base.AddListeners(); // GameEventBus.OnGameOver += OnGameOver;
            GameEventBus.OnGameUpdate += OnGameUpdate;
        }
        protected override void RemoveListeners()
        {
            GameEventBus.OnGameUpdate -= OnGameUpdate;
            base.RemoveListeners(); // GameEventBus.OnGameOver -= OnGameOver;
        }
        private void OnGameUpdate()
        {
            elapsedTime += Time.deltaTime;
            _stats.IncreaseTime(elapsedTime);
            survivalUI.SetElapsedTimeText(_stats.Time);
        }

        protected override void AddListenersToActors()
        {
            base.AddListenersToActors();
            LevelControllerConfigurator.AddEnemiesListeners(spawners, listeners);
        }
        protected override void RemoveListenersFromActors()
        {
            base.RemoveListenersFromActors();
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
        }

    }

}