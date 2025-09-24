using Depthcharge.Actors.AI;
using Depthcharge.LevelManagement;
using System.Collections;
using UnityEngine;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : BaseState
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
            level.Player.InputModule.DisableModule();
            level.SystemsRoot.UISystem.SetCampaignUIActiveness(false);
            StartCoroutine(ActivateWinUI());
        }

        public override void OnStateExit()
        {
            level.SystemsRoot.UISystem.SetWinUIActiveness(false);
            level.SystemsRoot.UISystem.WinUI.CleanUp();
        }

        private IEnumerator ActivateWinUI()
        {
            level.SystemsRoot.UISystem.SetWinUIActiveness(true);
            level.SystemsRoot.UISystem.WinUI.DeactivateMenu();
            level.SystemsRoot.UISystem.WinUI.FadeInPanel();
            yield return new WaitUntil(() => level.SystemsRoot.UISystem.WinUI.isPanelFadedIn);
            level.SystemsRoot.UISystem.WinUI.ActivateMenu();
            level.SystemsRoot.UISystem.WinUI.SetUp();
        }

    }

}