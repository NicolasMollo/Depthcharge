using System;

namespace Depthcharge.Actors
{

    public class EnemyListenersContainer
    {
        public Action OnSpawnEnemy;
        public Action<EnemyController> OnDefeatEnemy;
        public Action<EnemyController> OnDeactivateEnemy;

        public EnemyListenersContainer(Action OnSpawnEnemy, Action<EnemyController> OnDefeatEnemy, Action<EnemyController> OnDeactivateEnemy)
        {
            this.OnSpawnEnemy = OnSpawnEnemy;
            this.OnDefeatEnemy = OnDefeatEnemy;
            this.OnDeactivateEnemy = OnDeactivateEnemy;
        }
    }

}