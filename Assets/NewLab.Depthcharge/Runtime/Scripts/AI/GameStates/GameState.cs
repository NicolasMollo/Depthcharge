using Depthcharge.Actors.AI;
using Depthcharge.Events;
using Depthcharge.LevelManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class GameState : BaseState
    {

        private BaseLevelController level = null;
        private UISystem UISystem = null;

        public override void SetUp()
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
        }
        public override void OnStateEnter()
        {
            GameEventBus.CallOnGameStart();
            level = FindFirstObjectByType<BaseLevelController>();
            level.Player.EnableModules();
            UISystem.CurrentGameUI.gameObject.SetActive(true);
            AddListeners();
        }
        public override void OnStateUpdate()
        {
            GameEventBus.CallOnGameUpdate();
        }
        public override void OnStateExit()
        {
            RemoveListeners();
            UISystem.CurrentGameUI.gameObject.SetActive(false);
            level.Player.InputModule.DisableModule();
            GameEventBus.CallOnGameOver();
        }

        private void AddListeners()
        {
            level.Player.HealthModule.OnDeath += OnPlayerDeath;
            level.OnWin += OnLevelWin;
        }
        private void RemoveListeners()
        {
            level.OnWin -= OnLevelWin;
            level.Player.HealthModule.OnDeath -= OnPlayerDeath;
        }
        private void OnPlayerDeath()
        {
            fsm.ChangeState<LoseGameState>();
        }
        private void OnLevelWin()
        {
            fsm.ChangeState<WinGameState>();
        }

    }
}