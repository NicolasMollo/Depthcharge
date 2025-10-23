using Depthcharge.LevelManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    public class EnemySpawnerProvider : MonoBehaviour
    {

        private List<EnemySpawner> _spawners = null;
        public List<EnemySpawner> Spawners { get => _spawners; }
        private List<EnemyProvider> _providers = null;
        public List<EnemyProvider> Providers { get => _providers; }
        private List<StdEnemyController> _enemies = null;
        public List<StdEnemyController> Enemies { get => _enemies; }

        #region Factory

        [Header("FACTORIES")]

        [SerializeField]
        private BaseEnemySpawnerFactory weakESF = null;

        [SerializeField]
        private BaseEnemySpawnerFactory mediumESF = null;

        [SerializeField]
        private BaseEnemySpawnerFactory strongESF = null;

        [SerializeField]
        private BaseEnemySpawnerFactory randomESF = null;

        [SerializeField]
        private EnemySpawnerFactoryContext context = default;

        #endregion
        #region Settings

        [Header("SETTINGS")]

        [SerializeField]
        private List<Transform> targetTransform = null;

        private int poolSize = 0;

        #endregion

        public void SetUp(LevelConfiguration configuration, MovementDirection enemiesDirection)
        {
            poolSize = targetTransform.Count;
            context.EnemiesDirection = enemiesDirection;
            SetSpawners(configuration);
        }

        public void CleanUp()
        {
            foreach (EnemySpawner spawner in _spawners)
                spawner.CleanUp();
        }

        private void SetSpawners(LevelConfiguration configuration)
        {
            BaseEnemySpawnerFactory factory = GetSelectedFactory(configuration.Difficulty);
            _spawners = factory.CreateEnemySpawnerPool(context, poolSize);
            _providers = new List<EnemyProvider>();
            for (int i = 0; i < _spawners.Count; i++)
            {
                _spawners[i].transform.position = targetTransform[i].position;
                _spawners[i].SetUp(context.EnemiesDirection, configuration.MinEnemySpawnDelay, configuration.MaxEnemySpawnDelay);
                _providers.AddRange(_spawners[i].Providers);
            }
            _enemies = new List<StdEnemyController>();
            foreach (EnemyProvider provider in _providers)
            {
                _enemies.AddRange(provider.Enemies);
            }
        }

        private BaseEnemySpawnerFactory GetSelectedFactory(LevelConfiguration.LevelDifficulty difficulty)
        {
            BaseEnemySpawnerFactory selectedFactory = null;
            switch (difficulty)
            {
                case LevelConfiguration.LevelDifficulty.Easy:
                    selectedFactory = weakESF;
                    break;
                case LevelConfiguration.LevelDifficulty.Normal:
                    selectedFactory = mediumESF;
                    break;
                case LevelConfiguration.LevelDifficulty.Hard:
                    selectedFactory = strongESF;
                    break;
                case LevelConfiguration.LevelDifficulty.Random:
                    selectedFactory = randomESF;
                    break;
            }
            return selectedFactory;
        }

    }

}