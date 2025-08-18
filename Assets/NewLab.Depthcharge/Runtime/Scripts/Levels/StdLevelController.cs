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
        private WinConditionContainer selectedWinCondition = null;
        private List<EnemySpawner> enemySpawners = null;

        [SerializeField]
        private PlayerController player = null;
        [SerializeField]
        private EnemySpawnerProvider leftESP = null;
        [SerializeField]
        private EnemySpawnerProvider rightESP = null;

        protected override void SetUp()
        {
            _stats = new LevelStats();
            player.SetUp();
            systemsRoot.UISystem.GameUI.SetUp(player.HealthModule.HealthPercentage, player.ShootModule.PoolSize);
            systemsRoot.UISystem.GameUI.SetScoreText(_stats.Score.ToString());
            int levelNumber = gameLogic.IncreaseCurrentLevelNumber();
            configuration = gameLogic.GetLevelConfiguration();
            systemsRoot.UISystem.GameUI.SetLevelText(levelNumber.ToString());
            int randomIndex = Random.Range(0, configuration.WinCondition.Count);
            selectedWinCondition = configuration.WinCondition[randomIndex];
            systemsRoot.UISystem.GameUI.SetWinConditionText(configuration.Difficulty, selectedWinCondition.Description);
            systemsRoot.UISystem.GameUI.SetEnemiesText(_stats.EnemiesDefeated.ToString());
            SetUpEnemySpawners();
            AddListeners();
        }
        protected override void CleanUp()
        {
            RemoveListeners();
            foreach (EnemySpawner spawner in enemySpawners) 
                spawner.CleanUp();
            player.CleanUp();
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

            player.ShootModule.OnShoot += OnPlayerShoot;
            player.ShootModule.OnStartReload += OnPlayerStartReload;
            player.ShootModule.OnReloaded += OnPlayerReloaded;
            player.HealthModule.OnTakeDamage += OnPlayerTakeDamage;
            player.HealthModule.OnTakeHealth += OnPlayerTakeHealth;
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

            player.ShootModule.OnShoot -= OnPlayerShoot;
            player.ShootModule.OnStartReload -= OnPlayerStartReload;
            player.ShootModule.OnReloaded -= OnPlayerReloaded;
            player.HealthModule.OnTakeDamage -= OnPlayerTakeDamage;
            player.HealthModule.OnTakeHealth -= OnPlayerTakeHealth;
        }

        private void OnPlayerTakeDamage(float damage)
        {
            systemsRoot.UISystem.GameUI.UpdateHealthBar(player.HealthModule.HealthPercentage);
        }
        private void OnPlayerTakeHealth(float health)
        {
            systemsRoot.UISystem.GameUI.UpdateHealthBar(player.HealthModule.HealthPercentage);
        }

        private void OnPlayerShoot()
        {
            systemsRoot.UISystem.GameUI.AddAmmoTransparency();
        }
        private void OnPlayerStartReload(bool isReloading)
        {
            systemsRoot.UISystem.GameUI.DecreaseReloadBar(player.ShootModule.ReloadTime);
        }
        private void OnPlayerReloaded()
        {
            systemsRoot.UISystem.GameUI.RemoveAmmoTransparency();
            systemsRoot.UISystem.GameUI.ResetReloadBar();
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
            systemsRoot.UISystem.GameUI.SetScoreText(_stats.Score.ToString());
            systemsRoot.UISystem.GameUI.SetEnemiesText(_stats.EnemiesDefeated.ToString());
            if (selectedWinCondition.Strategy.WinLevelCondition(this))
            {
                _stats.DecreaseActiveEnemies();
                Debug.Log($"Enemies missed: {Stats.EnemiesMissed}");
                Time.timeScale = 0;
            }
        }

        #endregion

    }

}