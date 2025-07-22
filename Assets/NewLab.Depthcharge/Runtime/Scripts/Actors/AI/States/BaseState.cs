using UnityEngine;

namespace Depthcharge.Actors.AI
{

    public abstract class BaseState : MonoBehaviour
    {

        [SerializeField]
        protected Fsm fsm = null;

        [SerializeField]
        protected BaseState nextState = null;

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }

    }

}