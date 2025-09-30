using Depthcharge.Actors.AI;
using Depthcharge.GameManagement.AI;
using Depthcharge.LevelManagement;
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
            fsm.SetStartState();
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

        public void SetStateOnIdle()
        {
            IdleState idleState = fsm.GetState<IdleState>();
            if (idleState == null)
            {
                Debug.LogError($"=== {this.name} === \"IdleState\" doesn't exist!");
                return;
            }
            fsm.ChangeState(idleState);
            Debug.Log($"=== {this.name} === Game on {idleState.name}");
        }

        public void SetStateOnPreIdle()
        {
            PreIdleState preIdleState = fsm.GetState<PreIdleState>();
            if (preIdleState == null)
            {
                Debug.LogError($"=== {this.name} === \"PreIdleState\" doesn't exist!");
                return;
            }
            fsm.ChangeState(preIdleState);
            Debug.Log($"=== {this.name} === Game on {preIdleState.name}");
        }

        public void SetStateOnPreGame(BaseLevelController level)
        {
            PreGameState preGameState = fsm.GetState<PreGameState>();
            if (preGameState == null)
            {
                Debug.LogError($"=== {this.name} === \"PreGameState\" doesn't exist!");
                return;
            }
            preGameState.SetUp(level);
            fsm.ChangeState(preGameState);
            Debug.Log($"=== {this.name} === Game on {preGameState.name}");
        }
        public void SetStateOnGame(BaseLevelController level)
        {
            GameState gameState = fsm.GetState<GameState>();
            if (gameState == null)
            {
                Debug.LogError($"=== {this.name} === \"GameState\" doesn't exist!");
                return;
            }
            gameState.SetUp(level);
            fsm.ChangeState(gameState);
            Debug.Log($"=== {this.name} === Game on {gameState.name}");
        }
        public void SetStateOnWinGame(BaseLevelController level)
        {
            WinGameState winGameState = fsm.GetState<WinGameState>();
            if (winGameState == null)
            {
                Debug.LogError($"=== {this.name} === \"WinGameState\" doesn't exist!");
                return;
            }
            winGameState.SetUp(level);
            fsm.ChangeState(winGameState);
            Debug.Log($"=== {this.name} === Game on {winGameState.name}");
        }
        public void SetStateOnLoseGame(BaseLevelController level)
        {
            LoseGameState loseGameState = fsm.GetState<LoseGameState>();
            if (loseGameState == null)
            {
                Debug.LogError($"=== {this.name} === \"LoseGameState\" doesn't exist!");
                return;
            }
            loseGameState.SetUp(level);
            fsm.ChangeState(loseGameState);
            Debug.Log($"=== {this.name} === Game on {loseGameState.name}");
        }
        public void SetStateOnPause()
        {
            PauseState pauseState = fsm.GetState<PauseState>();
            if (pauseState == null)
            {
                Debug.LogError($"=== {this.name} === \"PauseState\" doesn't exist!");
                return;
            }
            fsm.ChangeState(pauseState);
            Debug.Log($"=== {this.name} === Game on {pauseState.name}");
        }

    }

}