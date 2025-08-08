using Codice.CM.Client.Differences;
using Depthcharge.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    public class StdLevelController : BaseLevelController
    {
        // Inherited members
        // --------------------------------------------
        // protected GameSystemsRoot systemsRoot = null;
        // protected GameLogic gameLogic = null;
        // protected LevelStats stats = null;
        // --------------------------------------------
        private LevelConfiguration configuration = null;
        private BaseWinStrategy selectedWinStrategy = null;
        private List<EnemySpawner> enemySpawners = null;

        [SerializeField]
        private EnemySpawnerProvider leftESP = null;
        [SerializeField]
        private EnemySpawnerProvider rightESP = null;


        protected override void SetUp()
        {
            _stats = new LevelStats();
            int randomIndex = Random.Range(0, configuration.WinStrategies.Count);
            selectedWinStrategy = configuration.WinStrategies[randomIndex];
            gameLogic.IncreaseCurrentLevelNumber();
            configuration = gameLogic.GetLevelConfiguration();
            SetUpEnemySpawners();
            AddListeners();
        }
        protected override void CleanUp()
        {
            RemoveListeners();
            foreach (EnemySpawner spawner in enemySpawners) 
                spawner.CleanUp();
        }

        private void SetUpEnemySpawners()
        {
            enemySpawners = new List<EnemySpawner>();
            bool reversedPositions = ReverseSpawnersPosition();
            leftESP.SetUp(
                configuration, 
                reversedPositions ? MovementDirection.Left : MovementDirection.Right
                );
            rightESP.SetUp(
                configuration, 
                reversedPositions ? MovementDirection.Right : MovementDirection.Left
                );
            enemySpawners.AddRange(leftESP.Spawners);
            enemySpawners.AddRange(rightESP.Spawners);
        }

        private bool ReverseSpawnersPosition()
        {
            int randomIndex = Random.Range(0, 2);
            if (randomIndex % 2 == 0)
            {
                Vector2 temporaryPosition = leftESP.transform.position;
                leftESP.transform.position = rightESP.transform.position;
                rightESP.transform.position = temporaryPosition;
                return true;
            }
            return false;
        }

        #region Events

        private void AddListeners()
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.OnSpawnEnemy += OnSpawnEnemy;
                foreach (EnemyProvider provider in spawner.Providers)
                    foreach (EnemyController enemy in provider.Enemies)
                    {
                        enemy.HealthModule.OnDeath += delegate { OnDefeatEnemy(enemy); };
                        enemy.OnDeactivation += OnDeactivateEnemy;
                    }
            }
        }
        private void RemoveListeners()
        {
            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.OnSpawnEnemy -= OnSpawnEnemy;
                foreach (EnemyProvider provider in spawner.Providers)
                    foreach (EnemyController enemy in provider.Enemies)
                    {
                        enemy.HealthModule.OnDeath -= delegate { OnDefeatEnemy(enemy); };
                        enemy.OnDeactivation -= OnDeactivateEnemy;
                    }
            }
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
            if (selectedWinStrategy.WinLevelCondition(this))
            {
                _stats.DecreaseActiveEnemies();
                Debug.Log($"Enemies missed: {Stats.EnemiesMissed}");
                Time.timeScale = 0;
                //activeEnemies--;
                // enemiesMissed = enemiesSpawned - EnemiesDefeated - activeEnemies;
            }
        }

        #endregion

    }

}