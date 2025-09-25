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
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            level.UIController.gameObject.SetActive(true);
            level.Player.HealthModule.OnDeath += OnPlayerDeath;
            level.OnWin += OnLevelWin;
            level.Player.EnableModules();
        }
        public override void OnStateExit()
        {
            level.OnWin -= OnLevelWin;
            level.Player.HealthModule.OnDeath -= OnPlayerDeath;
        }
        private void OnPlayerDeath()
        {
            stateManager.SetStateOnLoseGame(level);
        }
        private void OnLevelWin()
        {
            stateManager.SetStateOnWinGame(level);
        }
        //private void OnClickReloadButton(SceneConfiguration configuration)
        //{
        //    level.SystemsRoot.SceneSystem.ChangeScene(configuration);
        //}
        //private void OnClickQuitButton(SceneConfiguration configuration)
        //{
        //    level.SystemsRoot.SceneSystem.ChangeScene(configuration);
        //}

    }
}