using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.LevelManagement;
using Depthcharge.UI;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class LoadingGameState : BaseState
    {

        private BaseLevelController level = null;
        private BaseMovementAdapter playerMovement = null;
        private Transform cameraTransform = null;
        private UISystem UISystem = null;
        private GameLogic gameLogic = null;

        [Header("SETTINGS")]
        [SerializeField]
        private float playerTargetX = 0f;
        [SerializeField]
        private float playerSpeed = 0f;
        [SerializeField]
        private float cameraTargetY = 0f;
        [SerializeField]
        private float cameraSpeed = 0f;

        public override void SetUp()
        {
            UISystem = GameSystemsRoot.Instance.UISystem;
            gameLogic = GameLogic.Instance;
        }
        public override void OnStateEnter()
        {
            level = FindFirstObjectByType<BaseLevelController>();
            cameraTransform = Camera.main.transform;
            playerMovement = level.Player.GetComponentInChildren<BaseMovementAdapter>();
            UISystem.StartUI.DisableInput();
            UISystem.SetStartUIActiveness(false);
            UISystem.CurrentGameUI.gameObject.SetActive(false);
            level.Player.DisableModules();
            if (!gameLogic.LoadGameTransitionsState)
            {
                SetCameraToTargetPosition();
            }
            StartCoroutine(MoveCameraToTarget());
        }

        #region Camera

        private IEnumerator MoveCameraToTarget()
        {
            Vector2 direction = Vector2.up;
            float calculatedSpeed = 0.0f;
            Vector2 translation = Vector2.zero;
            while (cameraTransform.position.y < cameraTargetY)
            {
                calculatedSpeed = cameraSpeed * Time.deltaTime;
                translation = direction * calculatedSpeed;
                cameraTransform.Translate(translation);
                yield return null;
            }
            MovePlayerToTarget();
        }
        private void SetCameraToTargetPosition()
        {
            Vector3 targetPosition = new Vector3(
                cameraTransform.position.x,
                cameraTargetY,
                cameraTransform.position.z
                );
            cameraTransform.position = targetPosition;
        }

        #endregion
        #region Player

        private void MovePlayerToTarget()
        {
            if (playerMovement.GetPosition().x > playerTargetX)
            {
                StartCoroutine(MovePlayerLeft());
            }
            else
            {
                StartCoroutine(MovePlayerRight());
            }
        }
        private IEnumerator MovePlayerLeft()
        {
            float calculatedSpeed = 0.0f;
            Vector2 translation = Vector2.zero;
            while (playerMovement.GetPosition().x > playerTargetX)
            {
                calculatedSpeed = playerSpeed * Time.deltaTime;
                translation = Vector2.left * calculatedSpeed;
                playerMovement.Translate(translation);
                yield return null;
            }
            fsm.GoToTheNextState();
        }
        private IEnumerator MovePlayerRight()
        {
            float calculatedSpeed = 0.0f;
            Vector2 translation = Vector2.zero;
            while (playerMovement.GetPosition().x < playerTargetX)
            {
                calculatedSpeed = playerSpeed * Time.deltaTime;
                translation = Vector2.right * calculatedSpeed;
                playerMovement.Translate(translation);
                yield return null;
            }
            fsm.GoToTheNextState();
        }

        #endregion

    }
}