using Depthcharge.Actors.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    public class StdEnemy_AdvanceState : BaseState
    {

        private StdEnemyController _enemy = null;
        private MovementModule _movementModule = null;
        private float _travelledDistance = 0f;
        private float _randomDistanceOffset = 0f;
        private Vector3 _lastPosition = default;


        public override void SetUp(GameObject owner)
        {
            _enemy = owner.GetComponent<StdEnemyController>();
            string message = $"=== StdEnemy_advanceState.SetUp() === enemy is not a \"StdEnemyController\"!";
            Assert.IsNotNull(_enemy, message);
            _movementModule = _enemy.MovementModule;
        }

        public override void OnStateEnter()
        {
            _lastPosition = _movementModule.Target.GetPosition();
            SetRandomDistanceOffset();
        }
        public override void OnStateUpdate()
        {
            _movementModule.MoveTarget(_enemy.MovementDirection);
            CalculateTravelledDistance();
            float calculatedDistance = _enemy.TravelledDistanceToShoot + _randomDistanceOffset;
            if (_travelledDistance >= calculatedDistance)
            {
                SetRandomDistanceOffset();
                _travelledDistance = 0.0f;
                fsm.ChangeState<StdEnemy_AttackState>();
            }
        }

        private void CalculateTravelledDistance()
        {
            float positionDiff = Vector3.Distance(_movementModule.Target.GetPosition(), _lastPosition);
            _travelledDistance += positionDiff;
            _lastPosition = _movementModule.Target.GetPosition();
        }
        private void SetRandomDistanceOffset()
        {
            _randomDistanceOffset = Random.Range(_enemy.MinRandomDistance, _enemy.MaxRandomDistance);
        }

    }

}