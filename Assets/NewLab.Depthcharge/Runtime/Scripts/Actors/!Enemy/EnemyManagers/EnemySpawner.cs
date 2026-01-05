using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    [DisallowMultipleComponent]
    public class EnemySpawner : MonoBehaviour
    {

        private List<StdEnemyController> enemies = null;
        private float minSpawnDelay = 0.0f;
        private float maxSpawnDelay = 0.0f;
        private List<StdEnemyController> tierEnemies = null;
        private StdEnemyController.EnemyTier tier = StdEnemyController.EnemyTier.Last;

        private List<StdEnemyController> currentEnemies = null;

        [SerializeField]
        private List<EnemyProvider> _providers = null;
        public List<EnemyProvider> Providers { get => _providers; }

        public Action OnSpawnEnemy = null;


        public void SetUp(MovementDirection enemiesDirection, float minSpawnDelay, float maxSpawnDelay)
        {
            this.minSpawnDelay = minSpawnDelay;
            this.maxSpawnDelay = maxSpawnDelay;
            enemies = new List<StdEnemyController>();
            foreach (EnemyProvider provider in _providers)
            {
                provider.SetUp(enemiesDirection);
                foreach (StdEnemyController enemy in provider.Enemies)
                {
                    enemy.OnDeactivation += OnEnemyDeactivation;
                    enemies.Add(enemy);
                }
            }
            currentEnemies = enemies;
        }

        public void CleanUp()
        {
            foreach (EnemyProvider provider in _providers)
            {
                provider.CleanUp();
                foreach (StdEnemyController enemy in provider.Enemies)
                {
                    enemy.OnDeactivation -= OnEnemyDeactivation;
                }
            }
        }

        public void SetMinAndMaxSpawnDelay(float minSpawnDelay, float maxSpawnDelay)
        {
            if (this.minSpawnDelay == minSpawnDelay && this.maxSpawnDelay == maxSpawnDelay) return;
            this.minSpawnDelay = minSpawnDelay;
            this.maxSpawnDelay = maxSpawnDelay;
        }
        public void SetTierEnemies(StdEnemyController.EnemyTier tier)
        {
            if (this.tier == tier) return;
            this.tier = tier;
            tierEnemies = new List<StdEnemyController>();
            foreach (StdEnemyController enemy in enemies)
            {
                if (enemy.Tier == this.tier)
                {
                    tierEnemies.Add(enemy);
                }
            }
            currentEnemies = tierEnemies;
        }

        public IEnumerator SpawnEnemyDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            int randomIndex = UnityEngine.Random.Range(0, currentEnemies.Count);
            StdEnemyController enemyToSpawn = currentEnemies[randomIndex];
            SpawnEnemy(enemyToSpawn);
        }
        private void SpawnEnemy(StdEnemyController enemy)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.transform.SetParent(null);
                enemy.gameObject.SetActive(true);
                OnSpawnEnemy?.Invoke();
            }
        }

        private void OnEnemyDeactivation(BaseEnemyController enemy)
        {
            float randomDelay = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
            StartCoroutine(SpawnEnemyDelayed(randomDelay));
        }

    }

}