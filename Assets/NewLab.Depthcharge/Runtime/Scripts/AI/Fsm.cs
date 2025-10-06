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

        public void SetUpStates()
        {
            foreach (BaseState state in states)
            {
                state.SetUp();
            }
        }
        public void CleanUpStates()
        {
            foreach(BaseState state in states)
            {
                state.CleanUp();
            }
        }

        public void SetStartState()
        {
            currentState = startState;
            currentState.OnStateEnter();
        }
        public void UpdateCurrentState()
        {
            currentState.OnStateUpdate();
        }

        public void GoToTheNextState()
        {
            currentState.OnStateExit();
            currentState = currentState.NextState;
            if (currentState == null)
            {
                Debug.LogError($"=== {this.transform.parent.name}.FSM.GoToTheNextState() === Current state hasn't a next state!");
                return;
            }
            currentState.OnStateEnter();
        }
        public void ChangeState<T>() where T : BaseState
        {
            currentState.OnStateExit();
            currentState = GetState<T>();
            if (currentState == null)
            {
                Debug.LogError($"=== {this.transform.parent.name}.FSM === There isn't state of this type in the fsm!");
                return;
            }
            currentState.OnStateEnter();
        }

        private T GetState<T>() where T : BaseState
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