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
        private bool isOwnerGot = false;

        [Header("SETTINGS")]
        [SerializeField]
        private float targetPositionX = 0f;
        [SerializeField]
        private float movementSpeed = 0f;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
            // Camera.main.backgroundColor = Vector4.zero;
        }

        public override void OnStateEnter()
        {
            if (!isOwnerGot)
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
            level.SystemsRoot.UISystem.SetStartUIActiveness(false);
            level.UIController.gameObject.SetActive(false);
            level.Player.DisableModules();
            movementAdapter = level.Player.GetComponentInChildren<BaseMovementAdapter>();
            StartCoroutine(MovePlayerToPosition());
        }

        private IEnumerator MovePlayerToPosition()
        {
            Vector2 direction = movementAdapter.GetPosition().x < targetPositionX ? Vector2.right : Vector2.left;
            float offset = 0.1f;
            float positivePositionX = targetPositionX + offset;
            float negativePositionX = targetPositionX - offset;
            // Implement a method with single while cycle and single condition.
            while (movementAdapter.GetPosition().x < negativePositionX || movementAdapter.GetPosition().x > positivePositionX)
            {
                float calculatedSpeed = movementSpeed * Time.deltaTime;
                Vector2 translation = direction * calculatedSpeed;
                movementAdapter.Translate(translation);
                yield return null;
            }
            stateManager.SetStateOnGame(level);
        }

        // To subscribed on SettingsController which will be in UISystem.
        private void SetPlayerToTargetPosition()
        {
            Vector2 targetPosition = new Vector2(targetPositionX, movementAdapter.GetPosition().y);
            movementAdapter.MoveTo(targetPosition);
        }

    }
}