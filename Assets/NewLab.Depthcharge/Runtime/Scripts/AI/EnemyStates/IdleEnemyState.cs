using System.Collections;
using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public class IdleEnemyState : BaseState
    {

        private EnemyController enemyController = null;

        public override void SetUp()
        {
            enemyController = fsm.Owner.GetComponent<EnemyController>();
        }
        public override void OnStateEnter()
        {
            StartCoroutine(WaitToShoot());
        }

        private IEnumerator WaitToShoot()
        {
            yield return new WaitForSeconds(enemyController.ShootDelay);
            if (enemyController.ShootModule.IsReloading)
            {
                yield return new WaitUntil(() => enemyController.ShootModule.IsReloading);
            }
            fsm.GoToTheNextState();
        }

    }

}