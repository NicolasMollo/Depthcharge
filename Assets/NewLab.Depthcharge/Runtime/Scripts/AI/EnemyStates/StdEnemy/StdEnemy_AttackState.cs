using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    public class StdEnemy_AttackState : BaseState
    {

        private StdEnemyController _enemy = null;
        private MovementModule _movementModule = null;
        private float _shootCounter = 0f;


        public override void SetUp(GameObject owner)
        {
            _enemy = owner.GetComponent<StdEnemyController>();
            string message = $"=== StdEnemy_advanceState.SetUp() === enemy is not a \"StdEnemyController\"!";
            Assert.IsNotNull(_enemy, message);
            _movementModule = _enemy.MovementModule;
        }

        public override void OnStateEnter()
        {
            _shootCounter = _enemy.ShootDelay;
        }
        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_enemy.MovementDirection);
            _shootCounter -= Time.deltaTime;
            if (_shootCounter <= 0f)
            {
                _enemy.Shoot();
                fsm.ChangeState<StdEnemy_AdvanceState>();
            }
        }

    }

}