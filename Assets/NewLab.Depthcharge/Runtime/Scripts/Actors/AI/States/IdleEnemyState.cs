using System.Collections;
using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public class IdleEnemyState : BaseState
    {

        private EnemyController enemyController = null;
        private bool isEnemyControllerTaken = false;

        public override void OnStateEnter()
        {

            // Debug.Log($"I'm in {this.name}");
            if (!isEnemyControllerTaken)
            {
                enemyController = fsm.Owner.GetComponent<EnemyController>();
                isEnemyControllerTaken = true;
            }
            StartCoroutine(WaitToShoot());

        }

        private IEnumerator WaitToShoot()
        {

            yield return new WaitForSeconds(enemyController.ShootDelay);
            if (enemyController.ShootModule.IsReloading)
                yield return new WaitUntil(() => enemyController.ShootModule.IsReloading);
            fsm.ChangeState(nextState);

        }

    }

}