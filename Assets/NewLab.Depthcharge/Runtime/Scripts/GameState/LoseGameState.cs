using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    public class LoseGameState : BaseState
    {

        private GameStateManager stateManager = null;
        private BaseLevelController level = null;
        [SerializeField]
        private SceneConfiguration survivalScene = null;
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
            level.SystemsRoot.UISystem.LoseUI.Menu.SubscribeToReloadButton(OnClickButton);
            level.SystemsRoot.UISystem.LoseUI.Menu.SubscribeToQuitButton(OnClickButton);
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.SetCampaignUIActiveness(false);
            level.SystemsRoot.UISystem.SetSurvivalUIActiveness(false);
            StartCoroutine(ActivateLoseUI());
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            // level.SystemsRoot.UISystem.LoseUI.FadeOutPanel();
            //level.SystemsRoot.UISystem.LoseUI.FadeOutPanel();
            //level.SystemsRoot.UISystem.SetLoseUIActiveness(false);
            //level.SystemsRoot.SceneSystem.ChangeScene(configuration);
            level.SystemsRoot.UISystem.LoseUI.DisableInput();
            StartCoroutine(GoToTheNextState(configuration));
        }

        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
            level.SystemsRoot.UISystem.LoseUI.FadeOutPanel();
            yield return new WaitUntil(() => level.SystemsRoot.UISystem.LoseUI.isPanelFadedIn == false);
            level.SystemsRoot.UISystem.SetLoseUIActiveness(false);
            level.SystemsRoot.SceneSystem.ChangeScene(configuration);
        }

        public override void OnStateExit()
        {
            level.SystemsRoot.UISystem.LoseUI.Menu.SetAllFlaggedTextState(false);
            // level.SystemsRoot.UISystem.LoseUI.DisableInput();
            level.SystemsRoot.UISystem.SetLoseUIActiveness(false);
            level.SystemsRoot.UISystem.LoseUI.Menu.UnsubscribeFromReloadButton(OnClickButton);
            level.SystemsRoot.UISystem.LoseUI.Menu.UnsubscribeFromQuitButton(OnClickButton);
            // level.SystemsRoot.UISystem.LoseUI.CleanUp();
        }

        //private IEnumerator ActivateLoseUI()
        //{
        //    level.SystemsRoot.UISystem.SetLoseUIActiveness(true);
        //    level.SystemsRoot.UISystem.LoseUI.SetMenuActiveness(false);
        //    level.SystemsRoot.UISystem.LoseUI.FadeInPanel();
        //    yield return new WaitUntil(() => level.SystemsRoot.UISystem.LoseUI.isPanelFadedIn);
        //    level.SystemsRoot.UISystem.LoseUI.SetMenuActiveness(true);
        //    level.SystemsRoot.UISystem.LoseUI.SetUp();
        //}

        private IEnumerator ActivateLoseUI()
        {
            LoseUIController UI = level.SystemsRoot.UISystem.LoseUI;
            UI.SetDefeatedBlockActiveness(false);
            UI.SetMissedBlockActiveness(false);
            UI.SetScoreBlockActiveness(false);
            UI.SetTimeBlockActiveness(false);
            UI.Menu.SetQuitButtonActiveness(false);
            UI.Menu.SetReloadButtonActiveness(false);
            UI.SetSelectorActiveness(false);
            UI.SetMenuActiveness(false);
            level.SystemsRoot.UISystem.SetLoseUIActiveness(true);
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
            if (level.SystemsRoot.SceneSystem.CurrentScene.Configuration.SceneName == survivalScene.SceneName)
            {
                UI.SetTimeBlockActiveness(true);
                UI.SetTimeText($"{level.Stats.Time.ToString(@"hh\:mm\:ss")}");
                yield return new WaitUntil(() => UI.Menu.TimeTextSetState);
            }
            UI.EnableInput();
            UI.ResetSelection();
            UI.Menu.SetQuitButtonActiveness(true);
            UI.Menu.SetReloadButtonActiveness(true);
            UI.SetSelectorActiveness(true);
            // UI.SetUp();
        }

    }
}