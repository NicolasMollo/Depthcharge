using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : EndGameState
    {

        [SerializeField]
        [Tooltip("Delay in displaying the last level panel once the statistics are displayed")]
        private float endGamePanelAppearingDelay = 0.0f;
        [SerializeField]
        [Tooltip("Time during which the last level panel will be visible")]
        private float endGamePanelDisappearingDelay = 0.0f;

        protected override UI_EndGameController ConfigureUI()
        {
            return UISystem.WinUI;
        }
        public override void OnStateEnter()
        {
            if (gameLogic.GameProgression.IsLastLevel && gameLogic.GameProgression.PreviousLevelWasBoss)
            {
                level = FindFirstObjectByType<BaseLevelController>();
                UISystem.SetCurrentEndGameUI(UI);
                StartCoroutine(ActivateLastLevelUI());
            }
            else
            {
                base.OnStateEnter();
            }
        }

        protected override void OnClickReloadButton(SceneConfiguration configuration)
        {
            SceneConfiguration selectedConfiguration = configuration;
            if (!gameLogic.GameProgression.PreviousLevelWasBoss && gameLogic.GameProgression.IsBossLevel)
            {
                selectedConfiguration = bossSceneConfiguration;
                gameLogic.GameProgression.PreviousLevelWasBoss = true;
            }
            else
            {
                gameLogic.IncreaseLevelNumber();
                gameLogic.GameProgression.PreviousLevelWasBoss = false;
            }
            base.OnClickReloadButton(selectedConfiguration);
        }

        private IEnumerator ActivateLastLevelUI()
        {
            UI.SetBlocksActiveness(false);
            UI.SetButtonsActiveness(false);
            UI.SetSelectorActiveness(false);
            UI.SetMenuActiveness(false);
            UI.gameObject.SetActive(true);
            UI.FadeInPanel();
            level.AudioSource.SetVolume(0f, 0.05f);
            yield return new WaitUntil(() => UI.IsPanelFadedIn);
            UI.SetMenuActiveness(true);
            ConfigureTexts();
            yield return new WaitUntil(() => UI.AreMenuTextsConfigured);
            yield return new WaitForSeconds(endGamePanelAppearingDelay);
            UI.SetEndGamePanelOnBottomOfHierarchy();
            UI.FadeInEndGamePanel();
            yield return new WaitUntil(() => UI.LastLevelPanelFadedIn);
            UI.SetEndGameTextActiveness(true);
            UI.SetEndGameText(OnSetEndGameText);
            yield return new WaitUntil(() => UI.IsEndGameTextConfigured);
            yield return new WaitForSeconds(endGamePanelDisappearingDelay);
            fsm.ChangeState<LoadingIdleState>();
            UI.ResetEndGameText();
            gameLogic.GameProgression.PreviousLevelWasBoss = false;
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetLevelNumber();
        }

        private void OnSetEndGameText(bool isUpdatedText)
        {
            if (isUpdatedText)
            {
                audioSystem.PlayKeyPress();
            }
        }

    }

}