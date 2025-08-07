using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{

    public abstract class BaseEnemySpawnerFactory : ScriptableObject
    {

        public abstract List<EnemySpawner> CreateEnemySpawnerPool(EnemySpawnerFactoryContext context, int poolSize);
        protected abstract EnemySpawner CreateEnemySpawner(EnemySpawnerFactoryContext context);

    }

}