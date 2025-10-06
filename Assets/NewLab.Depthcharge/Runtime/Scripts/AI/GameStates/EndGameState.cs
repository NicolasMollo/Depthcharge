using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    public abstract class EndGameState : BaseState
    {

        protected BaseLevelController level = null;
        protected GameLogic gameLogic = null;
        protected SceneManagementSystem sceneSystem = null;
        protected UISystem UISystem = null;
        protected UI_EndGameController UI = null;

        public override void SetUp()
        {
            gameLogic = GameLogic.Instance;
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
            UISystem = GameSystemsRoot.Instance.UISystem;
            UI = ConfigureUI();
            UISystem.SetCurrentEndGameUI(UI);
        }
        public abstract UI_EndGameController ConfigureUI();
        public override void OnStateEnter()
        {
            level = FindFirstObjectByType<BaseLevelController>();
            AddListeners();
            StartCoroutine(ActivateEndGameUI());
        }
        public override void OnStateExit()
        {
            RemoveListeners();
            UI.SetAllTextsState(false);
        }

        private void AddListeners()
        {
            UI.SubscribeToButtons(OnClickButton);
            UI.SubscribeToButton(EndGameButtonType.Reload, OnClickReloadButton);
            UI.SubscribeToButton(EndGameButtonType.Quit, OnClickQuitButton);
        }
        private void RemoveListeners()
        {
            UI.UnsubscribeFromButton(EndGameButtonType.Quit, OnClickQuitButton);
            UI.UnsubscribeFromButton(EndGameButtonType.Reload, OnClickReloadButton);
            UI.UnsubscribeFromButtons(OnClickButton);
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            UI.DisableInput();
            StartCoroutine(OnSelectButton(configuration));
        }
        protected virtual void OnClickReloadButton(SceneConfiguration configuration)
        {
            gameLogic.LoadGameTransitionsState = false;
        }
        private void OnClickQuitButton(SceneConfiguration configuration)
        {
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetCurrentLevelNumber();
        }

        private IEnumerator OnSelectButton(SceneConfiguration configuration)
        {
            if (configuration.SceneName != sceneSystem.CurrentScene.Configuration.SceneName)
            {
                fsm.ChangeState<LoadingIdleState>();
                yield break;
            }
            UI.SetMenuActiveness(false);
            UI.FadeOutPanel();
            yield return new WaitUntil(() => UI.IsPanelFadedIn == false);
            UI.gameObject.SetActive(false);
            sceneSystem.ChangeScene(configuration);
            yield return new WaitUntil(() => sceneSystem.CurrentScene.IsLoaded);
            fsm.ChangeState<LoadingGameState>();
        }

        private IEnumerator ActivateEndGameUI()
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
            UI.EnableInput();
            UI.ResetSelection();
            UI.SetButtonsActiveness(true);
            UI.SetSelectorActiveness(true);
        }
        private void ConfigureTexts()
        {
            EndGameMenuTexts texts = new EndGameMenuTexts(
                level.Stats.EnemiesDefeated.ToString(),
                level.Stats.EnemiesMissed.ToString(),
                level.Stats.Score.ToString(),
                level.Stats.Time.ToString(@"hh\:mm\:ss")
            );
            bool isSurvival = level is SurvivalLevelController;
            UI.ConfigureTexts(texts, isSurvival);
        }

    }
}