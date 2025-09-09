using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    [DisallowMultipleComponent]
    public class GameState : BaseState
    {

        private BaseLevelController level = null;
        private GameStateManager stateManager = null;
        private bool isOwnerGot = false;

        public void SetUp(BaseLevelController level)
        {
            this.level = level;
        }
        public override void OnStateEnter()
        {
            if (!isOwnerGot)
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
            level.UIController.gameObject.SetActive(true);
            level.Player.HealthModule.OnDeath += OnPlayerDeath;
            level.Player.EnableModules();
        }
        private void OnPlayerDeath()
        {
            stateManager.SetStateOnPostGame();
        }

    }
}