using Depthcharge.Actors.AI;
using Depthcharge.Actors.Modules;
using Depthcharge.LevelManagement;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class PreGameState : BaseState
    {

        private BaseLevelController level = null;
        private BaseMovementAdapter movementAdapter = null;
        private GameStateManager stateManager = null;
        private Transform cameraTransform = null;
        private bool isOwnerGot = false;

        [Header("SETTINGS")]
        [SerializeField]
        private float playerTargetX = 0f;
        [SerializeField]
        private float playerSpeed = 0f;
        [SerializeField]
        private float cameraTargetY = 0f;
        [SerializeField]
        private float cameraSpeed = 0f;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
            cameraTransform = Camera.main.transform;
            // Camera.main.backgroundColor = Vector4.zero;
        }

        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            level.SystemsRoot.UISystem.StartUI.DisableInput();
            level.SystemsRoot.UISystem.SetStartUIActiveness(false);
            level.UIController.gameObject.SetActive(false);
            level.Player.DisableModules();
            movementAdapter = level.Player.GetComponentInChildren<BaseMovementAdapter>();
            if (!level.GameLogic.LoadGameTransitionsState)
                LoadWithoutTransitions();
            StartCoroutine(MoveCameraToTarget());
        }

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
        private void MovePlayerToTarget()
        {
            if (movementAdapter.GetPosition().x > playerTargetX)
                StartCoroutine(MovePlayerLeft());
            else
                StartCoroutine(MovePlayerRight());
        }
        private IEnumerator MovePlayerLeft()
        {
            float calculatedSpeed = 0.0f;
            Vector2 translation = Vector2.zero;
            while (movementAdapter.GetPosition().x > playerTargetX)
            {
                calculatedSpeed = playerSpeed * Time.deltaTime;
                translation = Vector2.left * calculatedSpeed;
                movementAdapter.Translate(translation);
                yield return null;
            }
            stateManager.SetStateOnGame(level);
        }
        private IEnumerator MovePlayerRight()
        {
            float calculatedSpeed = 0.0f;
            Vector2 translation = Vector2.zero;
            while (movementAdapter.GetPosition().x < playerTargetX)
            {
                calculatedSpeed = playerSpeed * Time.deltaTime;
                translation = Vector2.right * calculatedSpeed;
                movementAdapter.Translate(translation);
                yield return null;
            }
            stateManager.SetStateOnGame(level);
        }

        private void LoadWithoutTransitions()
        {
            SetCameraToTargetPosition();
            // SetPlayerToTargetPosition();
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
        private void SetPlayerToTargetPosition()
        {
            Vector2 targetPosition = new Vector2(playerTargetX, movementAdapter.GetPosition().y);
            movementAdapter.MoveTo(targetPosition);
        }

    }
}