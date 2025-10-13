using System.Collections;
using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public class IdleEnemyState : BaseState
    {

        protected BaseEnemyController enemyController = null;

        public override void SetUp()
        {
            enemyController = fsm.Owner.GetComponent<BaseEnemyController>();
        }
        public override void OnStateEnter()
        {
            StartCoroutine(WaitToShoot());
        }
        public override void OnStateExit()
        {
            StopAllCoroutines();
        }

        private IEnumerator WaitToShoot()
        {
            yield return new WaitForSeconds(enemyController.ShootDelay);
            if (enemyController.ShootModule.IsReloading)
            {
                yield return new WaitUntil(() => !enemyController.ShootModule.IsReloading);
            }
            fsm.GoToTheNextState();
        }

    }

}