using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors
{
    public abstract class BaseEnemyFactory : ScriptableObject
    {

        [SerializeField]
        protected EnemyConfiguration configuration = null;

        protected abstract EnemyController CreateEnemy(EnemyFactoryContext context);
        public abstract List<EnemyController> CreateEnemyPool(EnemyFactoryContext context, int poolSize);

    }

}