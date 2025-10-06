using Depthcharge.Actors.AI;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    [DisallowMultipleComponent]
    public class IdleState : BaseState
    {

        private UISystem UISystem = null;
        private SceneManagementSystem sceneSystem = null;

        public override void SetUp()
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
        }
        public override void OnStateEnter()
        {
            UISystem.SetCampaignUIActiveness(false);
            UISystem.SetStartUIActiveness(true);
            UISystem.StartUI.EnableInput();
            UISystem.StartUI.ResetSelection();
            UISystem.StartUI.SubscribeToSceneButtons(OnClickButton);
        }
        public override void OnStateExit()
        {
            UISystem.StartUI.UnsubscribeFromSceneButtons(OnClickButton);
        }

        private void OnClickButton(SceneConfiguration configuration)
        {
            UISystem.LoseUI.SetButtonArg(EndGameButtonType.Reload, configuration);
            StartCoroutine(GoToTheNextState(configuration));
        }

        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
            sceneSystem.ChangeScene(configuration);
            yield return new WaitUntil(() => sceneSystem.CurrentScene.IsLoaded);
            fsm.GoToTheNextState();
        }

    }

}