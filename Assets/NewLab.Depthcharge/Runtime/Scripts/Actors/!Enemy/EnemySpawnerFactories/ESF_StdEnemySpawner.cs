using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    [CreateAssetMenu(menuName = "Scriptable Objects/Actors/Enemy/EnemySpawnerFactories/ESF_StdEnemySpawner")]
    public class ESF_StdEnemySpawner : BaseEnemySpawnerFactory
    {

        [SerializeField]
        private GameObject prefabEnemySpawner = null;

        public override List<EnemySpawner> CreateEnemySpawnerPool(EnemySpawnerFactoryContext context, int poolSize)
        {
            List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
            EnemySpawner temporary = null;
            for (int i = 0; i < poolSize; i++)
            {
                temporary = CreateEnemySpawner(context);
                enemySpawners.Add(temporary);
            }
            return enemySpawners;
        }

        protected override EnemySpawner CreateEnemySpawner(EnemySpawnerFactoryContext context)
        {
            GameObject enemySpawnerObject = Instantiate(
                prefabEnemySpawner, 
                context.TargetPosition, 
                Quaternion.identity, 
                context.Parent);
            EnemySpawner enemySpawner = enemySpawnerObject.GetComponent<EnemySpawner>();
            return enemySpawner;
        }

    }

}