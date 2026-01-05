using Depthcharge.Actors.AI;
using Depthcharge.Audio;
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

        private UISystem _uiSystem = null;
        private AudioSystem _audioSystem = null;
        private SceneManagementSystem _sceneSystem = null;


        public override void SetUp(GameObject owner)
        {
            _uiSystem = GameSystemsRoot.Instance.UISystem;
            _audioSystem = GameSystemsRoot.Instance.AudioSystem;
            _sceneSystem = GameSystemsRoot.Instance.SceneSystem;
        }

        public override void OnStateEnter()
        {
            _uiSystem.SetCampaignUIActiveness(false);
            _uiSystem.SetStartUIActiveness(true);
            _uiSystem.StartUI.EnableInput();
            _uiSystem.StartUI.ResetSelection();
            _uiSystem.StartUI.SubscribeToSceneButtons(OnClickSceneButton);
            _uiSystem.StartUI.Selection.SubscribeOnSelectorPositioned(OnSelectorPositioned);
        }
        public override void OnStateExit()
        {
            _uiSystem.StartUI.Selection.UnsubscribeFromSelectorPositioned(OnSelectorPositioned);
            _uiSystem.StartUI.UnsubscribeFromSceneButtons(OnClickSceneButton);
        }

        private void OnSelectorPositioned()
        {
            _audioSystem.PlayHoverSfx();
        }
        private void OnClickSceneButton(SceneConfiguration configuration)
        {
            _uiSystem.LoseUI.SetButtonArg(EndGameButtonType.Reload, configuration);
            _audioSystem.PlayConfirmSfx();
            StartCoroutine(GoToTheNextState(configuration));
        }

        private IEnumerator GoToTheNextState(SceneConfiguration configuration)
        {
            _sceneSystem.ChangeScene(configuration);
            yield return new WaitUntil(() => _sceneSystem.CurrentScene.IsLoaded);
            fsm.ChangeToNextState();
        }

    }

}