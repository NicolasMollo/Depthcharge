using Codice.CM.Client.Differences;
using Depthcharge.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    public class StdLevelController : BaseLevelController
    {

        // Inherited members
        // protected GameSystemsRoot systemsRoot = null;

        [SerializeField]
        private LevelConfiguration _configuration = null;

        private int levelNumber = 0;

        private int enemiesDefeated = 0;
        private int enemiesSpawned = 0;
        private int activeEnemies = 0;
        private int enemiesMissed = 0;

        [SerializeField]
        [Range(1, 100)]
        private int enemiesToDefeat = 1;

        private List<EnemySpawner> enemySpawners = null;
        private List<EnemyProvider> enemyProviders = null;
        private List<EnemyController> enemies = null;

        [SerializeField]
        private EnemySpawnerProvider leftESP = null;
        [SerializeField]
        private EnemySpawnerProvider rightESP = null;


        protected override void SetUp()
        {
            SetUpEnemies();
            AddListeners();
        }

        private void SetUpEnemies()
        {
            enemiesToDefeat = _configuration.EnemyToDefeat;
            enemySpawners = new List<EnemySpawner>();
            enemyProviders = new List<EnemyProvider>();
            enemies = new List<EnemyController>();
            bool reversedPositions = ReverseSpawnersPosition();
            leftESP.SetUp(_configuration, reversedPositions ? MovementDirection.Left : MovementDirection.Right);
            rightESP.SetUp(_configuration, reversedPositions ? MovementDirection.Right : MovementDirection.Left);
            enemySpawners.AddRange(leftESP.Spawners);
            enemySpawners.AddRange(rightESP.Spawners);
            enemyProviders.AddRange(leftESP.Providers);
            enemyProviders.AddRange(rightESP.Providers);
            enemies.AddRange(leftESP.Enemies);
            enemies.AddRange(rightESP.Enemies);
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

        private void SetLevelNumber()
        {

        }

        private void SetLevelConfiguration()
        {

        }

        protected override void CleanUp()
        {
            RemoveListeners();
            foreach (EnemySpawner spawner in enemySpawners) 
                spawner.CleanUp();
        }

        private void AddListeners()
        {

            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.OnSpawnEnemy += OnSpawnEnemy;
            }
            //foreach (EnemyProvider provider in enemyProviders)
            //{
            //    foreach (EnemyController enemy in provider.Enemies)
            //    {
            //        enemy.HealthModule.OnDeath += OnDefeatEnemy;
            //        enemy.OnDeactivation += OnDeactivateEnemy;
            //    }
            //}
            foreach (EnemyController enemy in enemies)
            {
                enemy.HealthModule.OnDeath += OnDefeatEnemy;
                enemy.OnDeactivation += OnDeactivateEnemy;
            }

        }
        private void RemoveListeners()
        {

            foreach (EnemySpawner spawner in enemySpawners)
            {
                spawner.OnSpawnEnemy -= OnSpawnEnemy;
            }
            //foreach (EnemyProvider provider in enemyProviders)
            //{
            //    foreach (EnemyController enemy in provider.Enemies)
            //    {
            //        enemy.HealthModule.OnDeath -= OnDefeatEnemy;
            //        enemy.OnDeactivation -= OnDeactivateEnemy;
            //    }
            //}
            foreach (EnemyController enemy in enemies)
            {
                enemy.HealthModule.OnDeath -= OnDefeatEnemy;
                enemy.OnDeactivation -= OnDeactivateEnemy;
            }
        }


        private void OnDeactivateEnemy(EnemyController enemy)
        {
            activeEnemies--;
        }

        private void OnSpawnEnemy()
        {
            enemiesSpawned++;
            activeEnemies++;
        }

        private void OnDefeatEnemy()
        {
            enemiesDefeated++;
            Debug.Log($"Enemies defeated {enemiesDefeated}");
            if (WinCondition())
            {
                activeEnemies--;
                enemiesMissed = enemiesSpawned - enemiesDefeated - activeEnemies;
                Debug.Log($"Enemies spawned {enemiesSpawned}");
                Debug.Log($"Enemies defeated {enemiesDefeated}");
                Debug.Log($"Active enemies {activeEnemies}");
                Debug.Log($"Enemies missed {enemiesMissed}");
                Time.timeScale = 0;
            }
        }

        protected override bool WinCondition()
        {
            return enemiesDefeated == enemiesToDefeat;
        }

    }

}