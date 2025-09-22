using Depthcharge.Actors.AI;
using Depthcharge.UI;

namespace Depthcharge.GameManagement.AI
{

    public class WinGameState : BaseState
    {

        private UISystem UI = null;
        private GameStateManager stateManager = null;
        private bool isOwnerGot = false;

        public override void OnStateEnter()
        {
            if (!isOwnerGot)
            {
                stateManager = fsm.Owner.GetComponent<GameStateManager>();
                isOwnerGot = !isOwnerGot;
            }
            UI.SetWinUIActiveness(true);
        }

        public override void OnStateExit()
        {
            UI.SetWinUIActiveness(false);
        }

    }

}