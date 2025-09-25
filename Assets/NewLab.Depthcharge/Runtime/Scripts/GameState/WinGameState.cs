using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : BaseState
    {

        private BaseLevelController level = null;
        private GameStateManager stateManager = null;
        private bool isOwnerGot = false;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
        }

        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            level.SystemsRoot.UISystem.WinUI.Menu.SubscribeToReloadButton(OnClickButton);
            level.SystemsRoot.UISystem.WinUI.Menu.SubscribeToQuitButton(OnClickButton);
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.SetCampaignUIActiveness(false);
            StartCoroutine(ActivateWinUI());
        }

        public override void OnStateExit()
        {
            level.SystemsRoot.UISystem.WinUI.Menu.SetAllFlaggedTextState(false);
            // level.SystemsRoot.UISystem.WinUI.DisableInput();
            level.SystemsRoot.UISystem.SetWinUIActiveness(false);
            level.SystemsRoot.UISystem.WinUI.Menu.UnsubscribeFromReloadButton(OnClickButton);
            level.SystemsRoot.UISystem.WinUI.Menu.UnsubscribeFromQuitButton(OnClickButton);
            // level.SystemsRoot.UISystem.WinUI.CleanUp();
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            // level.SystemsRoot.UISystem.WinUI.FadeOutPanel();
            //level.SystemsRoot.UISystem.WinUI.FadeOutPanel();
            //level.SystemsRoot.UISystem.SetWinUIActiveness(false);
            //level.SystemsRoot.SceneSystem.ChangeScene(configuration);
            level.SystemsRoot.UISystem.WinUI.DisableInput();
            StartCoroutine(GoToTheNextState(configuration));
        }

        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
            level.SystemsRoot.UISystem.WinUI.FadeOutPanel();
            yield return new WaitUntil(() => level.SystemsRoot.UISystem.WinUI.isPanelFadedIn == false);
            level.SystemsRoot.UISystem.SetWinUIActiveness(false);
            level.SystemsRoot.SceneSystem.ChangeScene(configuration);
        }

        private IEnumerator ActivateWinUI()
        {
            WinUIController UI = level.SystemsRoot.UISystem.WinUI;
            UI.SetDefeatedBlockActiveness(false);
            UI.SetMissedBlockActiveness(false);
            UI.SetScoreBlockActiveness(false);
            UI.Menu.SetQuitButtonActiveness(false);
            UI.Menu.SetReloadButtonActiveness(false);
            UI.SetSelectorActiveness(false);
            UI.SetMenuActiveness(false);
            level.SystemsRoot.UISystem.SetWinUIActiveness(true);
            UI.FadeInPanel();
            yield return new WaitUntil(() => UI.isPanelFadedIn);
            UI.SetMenuActiveness(true);
            UI.SetDefeatedBlockActiveness(true);
            UI.SetDefeatedText(level.Stats.EnemiesDefeated.ToString());
            yield return new WaitUntil(() => UI.Menu.defeatedSetState);
            UI.SetMissedBlockActiveness(true);
            UI.SetMissedText(level.Stats.EnemiesMissed.ToString());
            yield return new WaitUntil(() => UI.Menu.MissedTextSetState);
            UI.SetScoreBlockActiveness(true);
            UI.SetScoreText(level.Stats.Score.ToString());
            yield return new WaitUntil(() => UI.Menu.ScoreTextSetState);
            UI.EnableInput();
            UI.ResetSelection();
            UI.Menu.SetQuitButtonActiveness(true);
            UI.Menu.SetReloadButtonActiveness(true);
            UI.SetSelectorActiveness(true);
            // UI.SetUp();
        }

    }

}