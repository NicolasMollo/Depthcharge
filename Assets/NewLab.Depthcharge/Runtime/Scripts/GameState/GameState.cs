using Depthcharge.Actors.AI;
using Depthcharge.Events;
using Depthcharge.LevelManagement;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class GameState : BaseState
    {

        private BaseLevelController level = null;
        private GameStateManager stateManager = null;

        private void Awake()
        {
            stateManager = fsm.Owner.GetComponent<GameStateManager>();
        }
        public void SetUp(BaseLevelController level)
        {
            this.level = level;
        }
        public override void OnStateEnter()
        {
            level.UIController.gameObject.SetActive(true);
            level.Player.EnableModules();
            level.Player.HealthModule.OnDeath += OnPlayerDeath;
            level.OnWin += OnLevelWin;
        }
        public override void OnStateUpdate()
        {
            GameEventBus.CallOnGameUpdate();
        }
        public override void OnStateExit()
        {
            level.OnWin -= OnLevelWin;
            level.Player.HealthModule.OnDeath -= OnPlayerDeath;
            GameEventBus.CallOnGameOver();
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.CurrentGameUI.gameObject.SetActive(false);
        }
        private void OnPlayerDeath()
        {
            stateManager.SetStateOnLoseGame(level);
        }
        private void OnLevelWin()
        {
            stateManager.SetStateOnWinGame(level);
        }

    }
}