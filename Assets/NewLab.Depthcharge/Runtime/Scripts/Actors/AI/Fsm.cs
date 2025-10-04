using System.Collections.Generic;
using UnityEngine;

namespace Depthcharge.Actors.AI
{

    [DisallowMultipleComponent]
    public class Fsm : MonoBehaviour
    {

        [SerializeField]
        private GameObject _owner = null;
        public GameObject Owner { get => _owner; }

        [SerializeField]
        private List<BaseState> states = null;

        [SerializeField]
        private BaseState startState = null;
        private BaseState currentState = null;

        public void SetStartState()
        {
            currentState = startState;
            currentState.OnStateEnter();
        }
        public void UpdateCurrentState()
        {
            currentState.OnStateUpdate();
        }
        public void ChangeState(BaseState state)
        {
            currentState.OnStateExit();
            currentState = state;
            currentState.OnStateEnter();
        }

        public T GetState<T>() where T : BaseState
        {
            foreach (BaseState state in states)
            {
                if (state is T typedState)
                {
                    return typedState;
                }
            }
            return null;
        }

    }

}