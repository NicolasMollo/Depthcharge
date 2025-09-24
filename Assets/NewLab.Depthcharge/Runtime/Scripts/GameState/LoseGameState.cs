using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{
    public class LoseGameState : BaseState
    {

        private GameStateManager stateManager = null;
        private BaseLevelController level = null;
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
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.SetCampaignUIActiveness(false);
            level.SystemsRoot.UISystem.SetSurvivalUIActiveness(false);
            StartCoroutine(ActivateLoseUI());
        }

        public override void OnStateExit()
        {
            level.SystemsRoot.UISystem.SetLoseUIActiveness(false);
            level.SystemsRoot.UISystem.LoseUI.CleanUp();
        }

        private IEnumerator ActivateLoseUI()
        {
            level.SystemsRoot.UISystem.SetLoseUIActiveness(true);
            level.SystemsRoot.UISystem.LoseUI.DeactivateMenu();
            level.SystemsRoot.UISystem.LoseUI.FadeInPanel();
            yield return new WaitUntil(() => level.SystemsRoot.UISystem.LoseUI.isPanelFadedIn);
            level.SystemsRoot.UISystem.LoseUI.ActivateMenu();
            level.SystemsRoot.UISystem.LoseUI.SetUp();
        }

    }
}