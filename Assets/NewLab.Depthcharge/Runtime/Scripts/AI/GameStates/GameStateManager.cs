using Depthcharge.Actors.AI;
using UnityEngine;

namespace Depthcharge.GameManagement
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance{ get; private set; } = null;

        [SerializeField]
        private Fsm fsm = null;

        private void Awake()
        {
            SetSingleton();
            DontDestroyOnLoad(this.gameObject);
        }
        private void SetSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        private void Start()
        {
            fsm.SetUpStates();
            fsm.SetStartState();
        }
        private void Update()
        {
            fsm.UpdateCurrentState();
        }
        private void OnDestroy()
        {
            fsm.CleanUpStates();
        }

        public void SetState<T>() where T : BaseState
        {
            fsm.ChangeState<T>();
        }

    }

}