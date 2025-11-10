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

        private bool previousLevelWasBoss = false;

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
            if (gameLogic.IsLastLevel && previousLevelWasBoss)
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
            if (!previousLevelWasBoss && gameLogic.IsBossLevel)
            {
                selectedConfiguration = bossSceneConfiguration;
                previousLevelWasBoss = true;
            }
            else
            {
                gameLogic.IncreaseCurrentLevelNumber();
                previousLevelWasBoss = false;
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
            yield return new WaitUntil(() => UI.IsPanelFadedIn);
            UI.SetMenuActiveness(true);
            ConfigureTexts();
            yield return new WaitUntil(() => UI.AreMenuTextsConfigured);
            yield return new WaitForSeconds(endGamePanelAppearingDelay);
            UI.FadeInEndGamePanel();
            yield return new WaitUntil(() => UI.LastLevelPanelFadedIn);
            UI.SetEndGameTextActiveness(true);
            UI.SetEndGameText();
            yield return new WaitUntil(() => UI.IsEndGameTextConfigured);
            yield return new WaitForSeconds(endGamePanelDisappearingDelay);
            fsm.ChangeState<LoadingIdleState>();
            UI.ResetEndGameText();
            previousLevelWasBoss = false;
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetCurrentLevelNumber();
        }

    }

}