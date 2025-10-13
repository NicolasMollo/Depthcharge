using Codice.CM.Client.Differences;
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
            SpawnEnemyWithRandomDelay();
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

        private void OnEnemyDeactivation(BaseEnemyController enemy)
        {
            SpawnEnemyWithRandomDelay();
        }

        private void SpawnEnemyWithRandomDelay()
        {
            float randomDelay = UnityEngine.Random.Range(minSpawnDelay, maxSpawnDelay);
            StartCoroutine(SpawnEnemyDelayed(randomDelay));
        }

        private IEnumerator SpawnEnemyDelayed(float delay)
        {
            yield return new WaitForSeconds(delay);
            int randomIndex = UnityEngine.Random.Range(0, enemies.Count);
            StdEnemyController enemyToSpawn = enemies[randomIndex];
            SpawnEnemy(enemyToSpawn);
        }

        private void SpawnEnemy(StdEnemyController enemy)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.transform.SetParent(null);
                enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
                OnSpawnEnemy?.Invoke();
            }
        }

    }

}