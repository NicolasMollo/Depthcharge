using Depthcharge.Actors.AI;
using Depthcharge.Audio;
using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.TimeManagement;
using Depthcharge.UI;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    [DisallowMultipleComponent]
    public class PauseState : BaseState
    {

        private UISystem UISystem = null;
        private AudioSystem audioSystem = null;
        private TimeSystem timeSystem = null;
        private BaseLevelController level = null;
        private GameLogic gameLogic = null;


        public override void SetUp(GameObject owner)
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            audioSystem = GameSystemsRoot.Instance.AudioSystem;
            timeSystem = GameSystemsRoot.Instance.TimeSystem;
            gameLogic = GameLogic.Instance;
        }

        public override void OnStateEnter()
        {
            level = FindFirstObjectByType<BaseLevelController>();
            level.AudioSource.Pause();
            UISystem.PauseUI.EnableInput();
            UISystem.PauseUI.ResetSelection();
            UISystem.SetPauseUIActiveness(true);
            AddListeners();
        }
        public override void OnStateExit()
        {
            RemoveListeners();
            UISystem.SetPauseUIActiveness(false);
            level.AudioSource.PlayCurrentClip();
            UISystem.PauseUI.DisableInput();
        }

        private void AddListeners()
        {
            timeSystem.OnSetTimeScale += OnSetTimeScale;
            UISystem.PauseUI.ResumeButton.OnClick += OnClickResumeButton;
            UISystem.PauseUI.QuitButton.OnClick += OnClickQuitButton;
            UISystem.PauseUI.Selection.SubscribeOnSelectorPositioned(OnSelectorPositioned);
        }
        private void RemoveListeners()
        {
            UISystem.PauseUI.Selection.UnsubscribeFromSelectorPositioned(OnSelectorPositioned);
            UISystem.PauseUI.QuitButton.OnClick -= OnClickQuitButton;
            UISystem.PauseUI.ResumeButton.OnClick -= OnClickResumeButton;
            timeSystem.OnSetTimeScale -= OnSetTimeScale;
        }

        private void OnClickResumeButton(int arg)
        {
            audioSystem.PlayConfirmSfx();
            timeSystem.SetTimeScale(1f);
        }
        private void OnClickQuitButton(SceneConfiguration config)
        {
            level.Stats.ResetAllStats();
            audioSystem.PlayCancelSfx();
            timeSystem.SetTimeScale(1f);
            audioSystem.SetGameSfxVolumes(0f);
            level.AudioSource.SetVolume(0f, 0.05f);
            fsm.ChangeState<LoadingIdleState>();
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetLevelNumber();
            gameLogic.GameProgression.PreviousLevelWasBoss = false;
        }

        private void OnSetTimeScale(float timeScale)
        {
            if (timeScale == 1)
            {
                fsm.ChangeState<GameState>();
            }
        }

        private void OnSelectorPositioned()
        {
            audioSystem.PlayHoverSfx();
        }

    }

}