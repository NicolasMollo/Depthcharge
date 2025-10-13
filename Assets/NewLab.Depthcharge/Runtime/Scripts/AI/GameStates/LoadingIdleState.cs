using Depthcharge.Actors.AI;
using Depthcharge.SceneManagement;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    public class LoadingIdleState : BaseState
    {

        private UISystem UISystem = null;
        private SceneManagementSystem sceneSystem = null;
        private bool isCameraOnTarget = false;

        [Header("SETTINGS")]
        [SerializeField]
        private SceneConfiguration idleSceneConfig = null;
        [SerializeField]
        private float cameraSpeed = 1.0f;
        [SerializeField]
        private float cameraTargetY = -10.0f;

        public override void SetUp()
        {
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
        }
        public override void OnStateEnter()
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            StartCoroutine(GoToIdleState());
        }
        public override void OnStateExit()
        {
            isCameraOnTarget = false;
        }

        private IEnumerator GoToIdleState()
        {
            UISystem.CurrentEndGameUI.SetMenuActiveness(false);
            UISystem.CurrentEndGameUI.FadeOutPanel();
            yield return new WaitUntil(() => !UISystem.CurrentEndGameUI.IsPanelFadedIn);
            UISystem.CurrentEndGameUI.gameObject.SetActive(false);
            StartCoroutine(MoveCameraToTarget());
            yield return new WaitUntil(() => isCameraOnTarget);
            sceneSystem.ChangeScene(idleSceneConfig);
            yield return new WaitUntil(() => sceneSystem.CurrentScene.IsLoaded);
            fsm.GoToTheNextState();
        }
        private IEnumerator MoveCameraToTarget()
        {
            Vector2 direction = Vector2.down;
            float calculatedSpeed = 0.0f;
            Vector2 traslation = Vector2.zero;
            while (Camera.main.transform.position.y >= cameraTargetY)
            {
                calculatedSpeed = cameraSpeed * Time.deltaTime;
                traslation = direction * calculatedSpeed;
                Camera.main.transform.transform.Translate(traslation);
                yield return null;
            }
            isCameraOnTarget = true;
        }

    }

}