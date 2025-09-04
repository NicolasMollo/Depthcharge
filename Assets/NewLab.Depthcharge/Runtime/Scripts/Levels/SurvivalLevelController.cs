using Depthcharge.Actors;
using Depthcharge.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    [DisallowMultipleComponent]
    public class SurvivalLevelController : BaseLevelController
    {

        private float elapsedTime = 0;
        private TimeSpan timeSpan = TimeSpan.Zero;
        private SurvivalUIController survivalUI;
        private EnemyListenersContainer listeners = null;

        private List<EnemySpawner> spawners = null;
        [SerializeField]
        private EnemySpawnerProvider[] spawnerProviders = null;

        protected override void SetUp()
        {
            base.SetUp();
            survivalUI = this.UI as SurvivalUIController;
            if (survivalUI == null)
            {
                Debug.LogError($"=== {this.gameObject.name} === UI type is wrong!");
                return;
            }
            systemsRoot.UISystem.SetSurvivalUIActiveness(true);
            spawners = LevelControllerConfigurator.SetEnemySpawners(Configuration, ref spawnerProviders);
            listeners = new EnemyListenersContainer(OnSpawnEnemy, OnDeactivateEnemy, OnDefeatEnemy);
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.SurvivalUI;
        }

        protected override void CleanUp()
        {
            base.CleanUp();
            foreach (EnemySpawner spawner in spawners)
                spawner.CleanUp();
            systemsRoot.UISystem.SetSurvivalUIActiveness(false);
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(elapsedTime);
            survivalUI.SetElapsedTimeText(timeSpan);
        }

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
        }

    }

}