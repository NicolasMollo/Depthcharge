using Depthcharge.Actors.AI;
using Depthcharge.Audio;
using Depthcharge.Events;
using Depthcharge.LevelManagement;
using Depthcharge.TimeManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class GameState : BaseState
    {

        private BaseLevelController level = null;
        private UISystem UISystem = null;
        private AudioSystem audioSystem = null;
        private TimeSystem timeSystem = null;

        public override void SetUp(GameObject owner)
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            audioSystem = GameSystemsRoot.Instance.AudioSystem;
            timeSystem = GameSystemsRoot.Instance.TimeSystem;
        }
        public override void OnStateEnter()
        {
            if (fsm.PreviousState is PauseState)
            {
                level.Player.EnableModules();
                return;
            }
            GameEventBus.CallOnGameStart();
            level = FindFirstObjectByType<BaseLevelController>();
            level.Player.EnableModules();
            UISystem.CurrentGameUI.gameObject.SetActive(true);
            audioSystem.SetGameSfxVolumes(1.0f);
            AddListeners();
        }
        public override void OnStateUpdate()
        {
            GameEventBus.CallOnGameUpdate();
        }
        public override void OnStateExit()
        {
            if (fsm.NextState is PauseState)
            {
                level.Player.InputModule.DisableModule();
                return;
            }
            RemoveListeners();
            UISystem.CurrentGameUI.gameObject.SetActive(false);
            level.Player.InputModule.DisableModule();
            level.Player.HealthModule.SetVulnerability(false);
            GameEventBus.CallOnGameOver();
        }

        private void AddListeners()
        {
            timeSystem.OnSetTimeScale += OnSetTimeScale;
            level.Player.HealthModule.OnDeath += OnPlayerDeath;
            level.OnWin += OnLevelWin;
        }
        private void RemoveListeners()
        {
            level.OnWin -= OnLevelWin;
            level.Player.HealthModule.OnDeath -= OnPlayerDeath;
            timeSystem.OnSetTimeScale -= OnSetTimeScale;
        }
        private void OnPlayerDeath()
        {
            fsm.ChangeState<LoseGameState>();
        }
        private void OnLevelWin()
        {
            fsm.ChangeState<WinGameState>();
        }
        private void OnSetTimeScale(float timeScale)
        {
            if (timeScale == 0)
            {
                fsm.ChangeState<PauseState>();
            }
        }

    }
}