using System;

namespace Depthcharge.Actors
{

    public class EnemyListenersContainer
    {
        public Action OnSpawnEnemy;
        public Action<BaseEnemyController> OnDefeatEnemy;
        public Action<BaseEnemyController> OnDeactivateEnemy;

        public EnemyListenersContainer(Action OnSpawnEnemy, Action<BaseEnemyController> OnDefeatEnemy, Action<BaseEnemyController> OnDeactivateEnemy)
        {
            this.OnSpawnEnemy = OnSpawnEnemy;
            this.OnDefeatEnemy = OnDefeatEnemy;
            this.OnDeactivateEnemy = OnDeactivateEnemy;
        }
    }

}