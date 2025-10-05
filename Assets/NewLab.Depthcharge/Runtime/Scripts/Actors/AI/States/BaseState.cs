using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public abstract class BaseState : MonoBehaviour
    {

        [SerializeField]
        protected Fsm fsm = null;

        [SerializeField]
        protected BaseState nextState = null;
        public BaseState NextState { get => nextState; }

        public virtual void SetUp() { }
        public virtual void CleanUp() { }
        public virtual void OnStateEnter() { }
        public virtual void OnStateUpdate() { }
        public virtual void OnStateExit() { }

    }

}