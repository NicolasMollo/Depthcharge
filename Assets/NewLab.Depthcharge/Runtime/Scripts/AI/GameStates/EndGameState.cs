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

        [SerializeField]
        protected SceneConfiguration bossSceneConfiguration = null;

        public override void SetUp(GameObject owner)
        {
            gameLogic = GameLogic.Instance;
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
            UISystem = GameSystemsRoot.Instance.UISystem;
            UI = ConfigureUI();
        }
        protected abstract UI_EndGameController ConfigureUI();
        public override void OnStateEnter()
        {
            level = FindFirstObjectByType<BaseLevelController>();
            UISystem.SetCurrentEndGameUI(UI);
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
            UI.SubscribeToButton(EndGameButtonType.Reload, OnClickReloadButton);
            UI.SubscribeToButton(EndGameButtonType.Quit, OnClickQuitButton);
        }
        private void RemoveListeners()
        {
            UI.UnsubscribeFromButton(EndGameButtonType.Quit, OnClickQuitButton);
            UI.UnsubscribeFromButton(EndGameButtonType.Reload, OnClickReloadButton);
        }

        private void OnClickQuitButton(SceneConfiguration configuration)
        {
            UI.DisableInput();
            fsm.ChangeState<LoadingIdleState>();
            gameLogic.LoadGameTransitionsState = true;
            gameLogic.ResetCurrentLevelNumber();
        }
        protected virtual void OnClickReloadButton(SceneConfiguration configuration)
        {
            UI.DisableInput();
            StartCoroutine(OnSelectReloadButton(configuration));
            gameLogic.LoadGameTransitionsState = false;
        }
        private IEnumerator OnSelectReloadButton(SceneConfiguration configuration)
        {
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
        protected void ConfigureTexts()
        {
            bool isBoss = level is BossLevelController;
            EndGameMenuTexts texts = new EndGameMenuTexts(
                level.Stats.EnemiesDefeated.ToString(),
                isBoss ? "0" : level.Stats.EnemiesMissed.ToString(),
                level.Stats.Score.ToString(),
                level.Stats.Time.ToString(@"hh\:mm\:ss")
            );
            bool isSurvival = level is SurvivalLevelController;
            UI.ConfigureTexts(texts, isSurvival);
        }

    }
}