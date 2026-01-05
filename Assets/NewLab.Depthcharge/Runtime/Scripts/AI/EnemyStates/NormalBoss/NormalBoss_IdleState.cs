using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public class NormalBoss_IdleState : BaseState
    {

        private float _counter = 0f;

        [SerializeField]
        [Tooltip("Time the boss will remain in idle state")]
        private float _idleStateTime = 0f;


        public override void OnStateEnter()
        {
            _counter = _idleStateTime;
        }

        public override void OnStateUpdate()
        {
            _counter -= Time.deltaTime;
            if (_counter <= 0f)
            {
                fsm.ChangeState<NormalBoss_RetreatState>();
            }
        }

    }

}