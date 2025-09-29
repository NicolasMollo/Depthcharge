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
        private UI_EndGameController UI = null;
        private bool isOwnerGot = false;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
            UI = level.SystemsRoot.UISystem.WinUI;
        }
        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            UI.SubscribeToButtons(OnClickButton);
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.SetCampaignUIActiveness(false);
            StartCoroutine(ActivateWinUI());
        }
        public override void OnStateExit()
        {
            UI.SetAllTextsState(false);
            UI.UnsubscribeFromButtons(OnClickButton);
            level.SystemsRoot.UISystem.SetWinUIActiveness(false);
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            UI.DisableInput();
            StartCoroutine(GoToTheNextState(configuration));
        }

        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
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