using Depthcharge.Actors;
using Depthcharge.Events;
using Depthcharge.Extensions;
using Depthcharge.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    public class CampaignLevelController : BaseLevelController
    {

        private List<EnemySpawner> spawners = null;
        private EnemyListenersContainer listeners = null;
        private float startSpawnDelay = 0.0f;
        [SerializeField]
        private EnemySpawnerProvider[] spawnerProviders = null;


        protected override void SetConfiguration(ref LevelConfiguration configuration)
        {
            configuration = gameLogic.GetCampaignConfiguration();
        }
        protected override void ConfigureUI(ref BaseGameUIController UI)
        {
            UI = systemsRoot.UISystem.CampaignUI;
        }
        protected override void SetGameUIContext(ref GameUIContext context)
        {
            base.SetGameUIContext(ref context);
            context.levelNumber = gameLogic.GameProgression.CurrentLevelNumber;
        }

        protected override void InternalSetUp()
        {
            spawners = LevelControllerConfigurator.SetEnemySpawners(_configuration, ref spawnerProviders);
            ListExtension.Shuffle(spawners);
            listeners = new EnemyListenersContainer(OnSpawnEnemy, OnDefeatEnemy, OnDeactivateEnemy);
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.gameObject.SetActive(false);
            }
        }
        protected override void InternalCleanUp()
        {
            base.InternalCleanUp();
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.CleanUp();
            }

            systemsRoot.UISystem.SetCampaignUIActiveness(false);
        }

        #region Events

        protected override void AddListeners()
        {
            base.AddListeners();
            GameEventBus.OnGameStart += OnGameStart;
        }
        protected override void RemoveListeners()
        {
            GameEventBus.OnGameStart -= OnGameStart;
            base.RemoveListeners();
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