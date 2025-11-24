using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Depthcharge.Actors.AI
{

    [DisallowMultipleComponent]
    public class Fsm : MonoBehaviour
    {

        [SerializeField]
        private GameObject _owner = null;
        [SerializeField]
        private List<BaseState> states = null;

        [SerializeField]
        private BaseState startState = null;
        private BaseState currentState = null;
        public BaseState CurrentState { get => currentState; }

        private void Awake()
        {
            string message = $"=== {this.transform.parent.name}.FSM.Awake() === Be ensure to assign fsm owner!";
            Assert.IsNotNull(_owner, message);
        }

        public void SetUpStates()
        {
            foreach (BaseState state in states)
            {
                state.SetUp(_owner);
            }
        }
        public void CleanUpStates()
        {
            foreach(BaseState state in states)
            {
                state.CleanUp(_owner);
            }
        }

        public void SetStartState(BaseState state = null)
        {
            if (state != null)
            {
                startState = state;
            }
            currentState = startState;
            currentState.OnStateEnter();
        }
        public void UpdateCurrentState()
        {
            currentState.OnStateUpdate();
        }

        public void ChangeToNextState()
        {
            currentState.OnStateExit();
            currentState = currentState.NextState;
            string message = $"=== {_owner.name}.FSM.GoToTheNextState() === Current state hasn't a next state!";
            Assert.IsNotNull(currentState, message);
            currentState.OnStateEnter();
        }
        public void ChangeState<T>() where T : BaseState
        {
            currentState.OnStateExit();
            currentState = GetState<T>();
            string message = $"=== {_owner.name}.FSM === There isn't state of this type in the fsm!";
            Assert.IsNotNull(currentState, message);
            currentState.OnStateEnter();
        }
        public void ChangeState(BaseState state)
        {
            string message = $"=== {_owner.name}.FSM.ChangeState() === \"state\" is null!";
            Assert.IsNotNull(state, message);
            currentState.OnStateExit();
            currentState = state;
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