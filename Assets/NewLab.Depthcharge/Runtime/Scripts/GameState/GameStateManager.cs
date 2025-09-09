using Depthcharge.Actors;
using Depthcharge.Actors.AI;
using Depthcharge.GameManagement.AI;
using Depthcharge.LevelManagement;
using Depthcharge.UI;
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
        public void SetStateOnPostGame()
        {
            PostGameState postGameState = fsm.GetState<PostGameState>();
            if (postGameState == null)
            {
                Debug.LogError($"=== {this.name} === \"PostGameState\" doesn't exist!");
                return;
            }
            fsm.ChangeState(postGameState);
            Debug.Log($"=== {this.name} === Game on {postGameState.name}");
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