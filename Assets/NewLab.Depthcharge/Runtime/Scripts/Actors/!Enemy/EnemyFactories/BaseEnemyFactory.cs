using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{
    public abstract class BaseEnemyFactory : ScriptableObject
    {

        [SerializeField]
        protected EnemyConfiguration configuration = null;

        protected abstract StdEnemyController CreateEnemy(EnemyFactoryContext context);
        public abstract List<StdEnemyController> CreateEnemyPool(EnemyFactoryContext context, int poolSize);

    }

}