using Depthcharge.Actors;
using Depthcharge.Events;
using Depthcharge.Extensions;
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
        private float startSpawnDelay = 0.0f;

        private List<EnemySpawner> spawners = null;
        [SerializeField]
        private EnemySpawnerProvider[] spawnerProviders = null;

        #region Difficulty settings

        [Header("DIFFICULTY SETTINGS")]
        [SerializeField]
        private float mediumScoreThreshold = 1.0f;
        [SerializeField]
        private float hardScoreThreshold = 1.0f;
        [SerializeField]
        private float minMediumSpawnDelay = 1.0f;
        [SerializeField]
        private float maxMediumSpawnDelay = 1.0f;
        [SerializeField]
        private float minHardSpawnDelay = 1.0f;
        [SerializeField]
        private float maxHardSpawnDelay = 1.0f;

        #endregion

        protected override void SetConfiguration(ref LevelConfiguration configuration)
        {
            configuration = gameLogic.GetSurvivalConfiguration();
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.SurvivalUI;
        }

        protected override void InternalSetUp()
        {
            survivalUI = this.UI as SurvivalUIController;
            if (survivalUI == null)
            {
                Debug.LogError($"=== {this.gameObject.name} === UI type is wrong!");
                return;
            }
            spawners = LevelControllerConfigurator.SetEnemySpawners(_configuration, ref spawnerProviders);
            ListExtension.Shuffle(spawners);
            listeners = new EnemyListenersContainer(OnSpawnEnemy, OnDefeatEnemy, OnDeactivateEnemy);
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.gameObject.SetActive(false);
            }
            SetSpawnersTier(StdEnemyController.EnemyTier.Weak);
        }
        protected override void InternalCleanUp()
        {
            base.InternalCleanUp();
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.CleanUp();
            }
            systemsRoot.UISystem.SetSurvivalUIActiveness(false);
        }

        protected override void AddListeners()
        {
            base.AddListeners(); // GameEventBus.OnGameOver += OnGameOver;
            GameEventBus.OnGameStart += OnGameStart;
            GameEventBus.OnGameUpdate += OnGameUpdate;
            _stats.OnIncreaseScore += OnIncreaseScore;
        }
        protected override void RemoveListeners()
        {
            _stats.OnIncreaseScore -= OnIncreaseScore;
            GameEventBus.OnGameUpdate -= OnGameUpdate;
            GameEventBus.OnGameStart -= OnGameStart;
            base.RemoveListeners(); // GameEventBus.OnGameOver -= OnGameOver;
        }


        private void OnGameStart()
        {
            _audioSource.PlayCurrentClip();
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.gameObject.SetActive(true);
                StartCoroutine(spawner.SpawnEnemyDelayed(startSpawnDelay));
                startSpawnDelay += Random.Range(0f, 8f);
            }
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
        private void OnDeactivateEnemy(BaseEnemyController enemy)
        {
            _stats.DecreaseActiveEnemies();
        }
        private void OnDefeatEnemy(BaseEnemyController enemy)
        {
            _stats.IncreaseEnemiesDefeated();
            _stats.IncreaseScore(enemy.ScorePoints);
            UI.SetScoreText(_stats.Score.ToString());
            UI.SetEnemiesText(_stats.EnemiesDefeated.ToString());
        }
        private void OnIncreaseScore(float score)
        {
            if (score >= hardScoreThreshold)
            {
                SetSpawnersTier(StdEnemyController.EnemyTier.Strong);
                SetSpawnersDelays(minHardSpawnDelay, maxHardSpawnDelay);
            }
            else if (score >= mediumScoreThreshold)
            {
                SetSpawnersTier(StdEnemyController.EnemyTier.Medium);
                SetSpawnersDelays(minMediumSpawnDelay, maxMediumSpawnDelay);
            }
        }

        private void SetSpawnersTier(StdEnemyController.EnemyTier tier)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SetTierEnemies(tier);
            }
        }
        private void SetSpawnersDelays(float minSpawnDelay, float maxSpawnDelay)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.SetMinAndMaxSpawnDelay(minSpawnDelay, maxSpawnDelay);
            }
        }

    }

}