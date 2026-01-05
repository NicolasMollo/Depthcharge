using Depthcharge.Actors.AI;
using Depthcharge.SceneManagement;
using Depthcharge.Toolkit;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    public class LoadingIdleState : BaseState
    {

        private UI_EndGameController endGameUI = null;
        private SceneManagementSystem sceneSystem = null;
        private Transform cameraTransform = null;

        [Header("SETTINGS")]
        [SerializeField]
        private SceneConfiguration idleSceneConfig = null;
        [SerializeField]
        private float cameraTargetY = -10.0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float cameraSpeed = 1.0f;


        public override void SetUp(GameObject owner)
        {
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
        }
        public override void OnStateEnter()
        {
            endGameUI = GameSystemsRoot.Instance.UISystem.CurrentEndGameUI;
            cameraTransform = Camera.main.transform;
            StartCoroutine(GoToIdleState());
        }

        private IEnumerator GoToIdleState()
        {
            if (fsm.PreviousState is EndGameState)
            {
                yield return HandleEndGameUI();
            }
            Vector3 cameraTarget = new Vector3(cameraTransform.position.x, cameraTargetY, cameraTransform.position.z);
            yield return TransformTween.MoveToTarget(cameraTransform, cameraTarget, cameraSpeed);
            sceneSystem.ChangeScene(idleSceneConfig);
            yield return new WaitUntil(() => sceneSystem.CurrentScene.IsLoaded);
            fsm.ChangeToNextState();
        }

        private IEnumerator HandleEndGameUI()
        {
            endGameUI.SetMenuActiveness(false);
            endGameUI.FadeOutPanel();
            yield return new WaitUntil(() => !endGameUI.IsPanelFadedIn);
            if (endGameUI.LastLevelPanelFadedIn)
            {
                endGameUI.SetEndGameTextActiveness(false);
                endGameUI.FadeOutEndGamePanel();
                yield return new WaitUntil(() => !endGameUI.LastLevelPanelFadedIn);
                endGameUI.SetEndGamePanelOnTopOfHierarchy();
            }
            endGameUI.gameObject.SetActive(false);
        }

    }

}