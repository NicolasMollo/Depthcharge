using Depthcharge.Actors.AI;
using Depthcharge.SceneManagement;
using Depthcharge.UI.EndGame;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    public class PreIdleState : BaseState
    {

        private bool isCameraOnTarget = false;
        private Camera mainCamera = null;
        private UI_EndGameController UI = null;
        private GameStateManager stateManager = null;
        private SceneManagementSystem sceneSystem = null;
        [SerializeField]
        private SceneConfiguration idleSceneConfig = null;
        [SerializeField]
        private float cameraSpeed = 1.0f;
        [SerializeField]
        private float cameraTargetY = -10.0f;

        public override void OnStateEnter()
        {
            mainCamera = Camera.main;
            UI = GameSystemsRoot.Instance.UISystem.CurrentEndGameUI;
            sceneSystem = GameSystemsRoot.Instance.SceneSystem;
            stateManager = GameStateManager.Instance;
            StartCoroutine(GoToIdleState());
        }
        public override void OnStateExit()
        {
            isCameraOnTarget = false;
        }

        private IEnumerator GoToIdleState()
        {
            UI.SetMenuActiveness(false);
            UI.FadeOutPanel();
            yield return new WaitUntil(() => !UI.IsPanelFadedIn);
            StartCoroutine(MoveCameraToTarget());
            yield return new WaitUntil(() => isCameraOnTarget);
            sceneSystem.ChangeScene(idleSceneConfig);
            stateManager.SetStateOnIdle();
        }

        private IEnumerator MoveCameraToTarget()
        {
            Vector2 direction = Vector2.down;
            float calculatedSpeed = 0.0f;
            Vector2 traslation = Vector2.zero;
            while (mainCamera.transform.position.y >= cameraTargetY)
            {
                calculatedSpeed = cameraSpeed * Time.deltaTime;
                traslation = direction * calculatedSpeed;
                mainCamera.transform.transform.Translate(traslation);
                yield return null;
            }
            isCameraOnTarget = true;
        }

    }

}