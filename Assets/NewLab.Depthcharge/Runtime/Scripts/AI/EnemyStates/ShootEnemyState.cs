using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    public class ShootEnemyState : BaseState
    {

        private BaseEnemyController enemy = null;

        public override void SetUp(GameObject owner)
        {
            enemy = owner.GetComponent<BaseEnemyController>();
            string message = $"=== {owner.name}.ShootEnemyState.SetUp() === Owner is not a \"BaseEnemyController\"!";
            Assert.IsNotNull(enemy, message);
        }
        public override void OnStateEnter()
        {
            enemy.Shoot();
            fsm.ChangeToNextState();
        }

    }

}