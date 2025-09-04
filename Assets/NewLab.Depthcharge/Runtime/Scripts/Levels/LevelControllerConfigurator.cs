using Codice.CM.Client.Differences;
using Depthcharge.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.LevelManagement
{

    public static class LevelControllerConfigurator
    {

        public static List<EnemySpawner> SetEnemySpawners(LevelConfiguration configuration, ref EnemySpawnerProvider[] spawners)
        {
            List<EnemySpawner> newSpawners = new List<EnemySpawner>();
            bool reversedPositions = ReverseSpawnersPosition(spawners);
            spawners[0].SetUp(configuration, reversedPositions ? MovementDirection.Left : MovementDirection.Right);
            spawners[1].SetUp(configuration, reversedPositions ? MovementDirection.Right : MovementDirection.Left);
            newSpawners.AddRange(spawners[0].Spawners);
            newSpawners.AddRange(spawners[1].Spawners);
            return newSpawners;
        }

        private static bool ReverseSpawnersPosition(EnemySpawnerProvider[] spawners)
        {
            int randomIndex = UnityEngine.Random.Range(0, 2);
            if (randomIndex % 2 == 0)
            {
                Vector2 temporaryPosition = spawners[0].transform.position;
                spawners[0].transform.position = spawners[1].transform.position;
                spawners[1].transform.position = temporaryPosition;
                return true;
            }
            return false;
        }

        public static void AddEnemiesListeners(List<EnemySpawner> spawners, EnemyListenersContainer listeners)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.OnSpawnEnemy += listeners.OnSpawnEnemy;
                foreach (EnemyProvider provider in spawner.Providers)
                    foreach (EnemyController enemy in provider.Enemies)
                    {
                        enemy.HealthModule.OnDeath += delegate { listeners.OnDefeatEnemy(enemy); };
                        enemy.OnDeactivation += listeners.OnDeactivateEnemy;
                    }
            }
        }
        public static void RemoveEnemiesListeners(List<EnemySpawner> spawners, EnemyListenersContainer listeners)
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.OnSpawnEnemy -= listeners.OnSpawnEnemy;
                foreach (EnemyProvider provider in spawner.Providers)
                    foreach (EnemyController enemy in provider.Enemies)
                    {
                        enemy.HealthModule.OnDeath -= delegate { listeners.OnDefeatEnemy(enemy); };
                        enemy.OnDeactivation -= listeners.OnDeactivateEnemy;
                    }
            }
        }

    }

}