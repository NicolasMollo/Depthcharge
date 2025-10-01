using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : BaseState
    {

        private BaseLevelController level = null;
        private GameStateManager stateManager = null;
        private GameLogic gameLogic = null;
        private UI_EndGameController UI = null;
        private bool isOwnerGot = false;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
            UI = level.SystemsRoot.UISystem.WinUI;
            gameLogic = GameLogic.Instance;
        }
        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            UI.SubscribeToButtons(OnClickButton);
            UI.SubscribeToButton(EndGameButtonType.Reload, OnClickReloadButton);
            UI.SubscribeToButton(EndGameButtonType.Quit, OnClickQuitButton);
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.CurrentGameUI.gameObject.SetActive(false);
            StartCoroutine(ActivateWinUI());
        }
        public override void OnStateExit()
        {
            UI.SetAllTextsState(false);
            UI.UnsubscribeFromButton(EndGameButtonType.Quit, OnClickQuitButton);
            UI.UnsubscribeFromButton(EndGameButtonType.Reload, OnClickReloadButton);
            UI.UnsubscribeFromButtons(OnClickButton);
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            UI.DisableInput();
            StartCoroutine(GoToTheNextState(configuration));
        }
        private void OnClickReloadButton(SceneConfiguration configuration)
        {
            gameLogic.LoadGameTransitionsState = false;
            gameLogic.IncreaseCurrentLevelNumber();
        }
        private void OnClickQuitButton(SceneConfiguration configuration)
        {
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetCurrentLevelNumber();
        }
        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
            if (configuration.SceneName != level.SystemsRoot.SceneSystem.CurrentScene.Configuration.SceneName)
            {
                stateManager.SetStateOnPreIdle();
                yield break;
            }
            UI.SetMenuActiveness(false);
            UI.FadeOutPanel();
            yield return new WaitUntil(() => UI.IsPanelFadedIn == false);
            level.SystemsRoot.UISystem.SetWinUIActiveness(false);
            level.SystemsRoot.SceneSystem.ChangeScene(configuration);
        }

        private IEnumerator ActivateWinUI()
        {
            UI.SetBlocksActiveness(false);
            UI.SetButtonsActiveness(false);
            UI.SetSelectorActiveness(false);
            UI.SetMenuActiveness(false);
            level.SystemsRoot.UISystem.SetWinUIActiveness(true);
            UI.FadeInPanel();
            yield return new WaitUntil(() => UI.IsPanelFadedIn);
            UI.SetMenuActiveness(true);
            EndGameMenuTexts endGameTexts = new EndGameMenuTexts(
                level.Stats.EnemiesDefeated.ToString(), 
                level.Stats.EnemiesMissed.ToString(), 
                level.Stats.Score.ToString());
            UI.ConfigureTexts(endGameTexts);
            yield return new WaitUntil(() => UI.AreMenuTextsConfigured);
            UI.EnableInput();
            UI.ResetSelection();
            UI.SetButtonsActiveness(true);
            UI.SetSelectorActiveness(true);
        }

    }

}