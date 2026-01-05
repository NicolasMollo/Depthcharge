using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using Depthcharge.Toolkit;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class LoadingGameState : BaseState
    {

        private BaseLevelController level = null;
        private UISystem UISystem = null;
        private GameLogic gameLogic = null;
        private Transform playerTransform = null;
        private Transform cameraTransform = null;

        [Header("SETTINGS")]
        [SerializeField]
        private float playerTargetX = 0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float playerSpeed = 1f;
        [SerializeField]
        private float cameraTargetY = 0f;
        [SerializeField]
        [Range(1.0f, 10.0f)]
        private float cameraSpeed = 1f;


        public override void SetUp(GameObject owner)
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            gameLogic = GameLogic.Instance;
        }
        public override void OnStateEnter()
        {
            level = FindFirstObjectByType<BaseLevelController>();
            cameraTransform = Camera.main.transform;
            playerTransform = level.Player.transform;
            UISystem.StartUI.DisableInput();
            UISystem.SetStartUIActiveness(false);
            UISystem.CurrentGameUI.gameObject.SetActive(false);
            level.Player.DisableModules();
            if (!gameLogic.LoadGameTransitionsState)
            {
                SetCameraToTargetPosition();
            }
            StartCoroutine(MoveCameraAndPlayerToTarget());
        }

        private IEnumerator MoveCameraAndPlayerToTarget()
        {
            Vector3 cameraTarget = new Vector3(cameraTransform.position.x, cameraTargetY, cameraTransform.position.z);
            yield return TransformTween.MoveToTarget(cameraTransform, cameraTarget, cameraSpeed);
            Vector3 playerTarget = new Vector3(playerTargetX, playerTransform.position.y, playerTransform.position.z);
            yield return TransformTween.MoveToTarget(playerTransform, playerTarget, playerSpeed);
            fsm.ChangeToNextState();
        }
        private void SetCameraToTargetPosition()
        {
            Vector3 targetPosition = new Vector3(
                cameraTransform.transform.position.x,
                cameraTargetY,
                cameraTransform.transform.position.z
                );
            cameraTransform.transform.position = targetPosition;
        }

    }
}