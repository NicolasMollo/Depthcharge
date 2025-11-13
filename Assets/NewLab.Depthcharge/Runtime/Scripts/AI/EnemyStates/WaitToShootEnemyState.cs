using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    public class WaitToShootEnemyState : BaseState
    {

        protected BaseEnemyController enemy = null;

        public override void SetUp(GameObject owner)
        {
            enemy = owner.GetComponent<BaseEnemyController>();
            string message = $"=== {owner.name}.WaitToShootEnemyState.SetUp() === Owner is not a \"BaseEnemyController\"!";
            Assert.IsNotNull(enemy, message);
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
            yield return new WaitForSeconds(enemy.ShootDelay);
            if (enemy.ShootModule.IsReloading)
            {
                yield return new WaitUntil(() => !enemy.ShootModule.IsReloading);
            }
            fsm.ChangeState<ShootEnemyState>();
        }

    }

}